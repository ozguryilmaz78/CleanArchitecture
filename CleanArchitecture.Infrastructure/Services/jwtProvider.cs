﻿using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class JwtProvider(
        UserManager<AppUser> userManager,
        IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        public async Task<LoginCommandResponse> CreateToken(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.FullName),
                new Claim("Email", user.Email ?? ""),
                new Claim("UserName", user.UserName ?? "")
            };

            DateTime expires = DateTime.UtcNow.AddMonths(1);


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

            JwtSecurityTokenHandler handler = new();

            string token = handler.WriteToken(jwtSecurityToken);

            string refreshToken = Guid.NewGuid().ToString();
            DateTime refreshTokenExpires = expires.AddHours(1);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = refreshTokenExpires;

            await userManager.UpdateAsync(user);

            return new(token, refreshToken, refreshTokenExpires);
        }

        public async Task<EmailConfirmationCommandResponse> CreateEmailConfirmationToken(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim("UserName", user.UserName.ToString()),
                new Claim("Email", user.Email ?? "")
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

            JwtSecurityTokenHandler handler = new();

            string emailConfirmationToken = handler.WriteToken(jwtSecurityToken);
            user.EmailConfirmationCode = emailConfirmationToken;
            user.SecurityStamp = Guid.NewGuid().ToString();
            await userManager.UpdateAsync(user);

            return new(emailConfirmationToken);
        }
    }
}
