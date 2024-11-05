﻿using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtProvider jwtProvider;

        public LoginCommandHandler(
            UserManager<User> _userManager, 
            SignInManager<User> _signInManager,
            IJwtProvider _jwtProvider)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            jwtProvider = _jwtProvider;
        }

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users
                         .FirstOrDefaultAsync(p =>
                         p.UserName == request.EmailOrUserName ||
                         p.Email == request.EmailOrUserName,
                         cancellationToken);

            if (user is null)
            {
                return (500, "Kullanıcı bulunamadı.");
            }

            SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (signInResult.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
                if (timeSpan is not null)
                    return (500, $"Şifrenizi 3 defa yanlış girdiğiniz için kullanıcı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir");
                else
                    return (500, "Kullanıcınız 3 kez yanlış şifre girdiği için 5 dakika süreyle bloke edilmiştir");
            }

            if (signInResult.IsNotAllowed)
            {
                return (500, "Mail adresiniz onaylı değil");
            }

            if (!signInResult.Succeeded)
            {
                return (500, "Şifreniz yanlış");
            }

            var loginResponse = await jwtProvider.CreateToken(user);
            return Result<LoginCommandResponse>.Succeed(loginResponse);
        }
    }
}
