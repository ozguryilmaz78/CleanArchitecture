﻿using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;

namespace CleanArchitecture.Application.Features.Auth.Register
{
    public partial class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UpdateCommandResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        

        public RegisterCommandHandler(
            UserManager<AppUser> userManager,
            IJwtProvider jwtProvider,
            IEmailService emailService,
            IConfiguration configuration,
            IHostingEnvironment environment)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<Result<UpdateCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userImageFolder = _configuration["ManagementSettings:UserImageFolder"];
                var BackendUrl = _configuration["ManagementSettings:BackendUrl"];
                AppUser? user = await _userManager.Users
                            .FirstOrDefaultAsync(p =>
                            (p.UserName == request.UserName ||
                            p.Email == request.Email) &&
                            !p.IsDeleted,
                            cancellationToken);

                if (user != null)
                {
                    return (500, "Kullanıcı adı veya email sistemde kayıtlı.");
                }

                user = new AppUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    FullName = request.FirstName + " " + request.LastName,
                    UserName = request.UserName,
                    Email = request.Email
                };

                var emailResponse = await _jwtProvider.CreateEmailConfirmationToken(user);
                user.EmailConfirmationCode = emailResponse.Token;
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Result<UpdateCommandResponse>.Failure(500, $"Kayıt başarısız: {errors}");
                }

                if (request.PhotoFile != null)
                {
                    var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var fileName = $"{user.Id}_{timestamp}.jpg";
                    var filePath = Path.Combine(Path.Combine(_environment.WebRootPath, userImageFolder), fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        request.PhotoFile.CopyTo(stream);
                    }
                    user.PhotoUrl = $"{BackendUrl}/{userImageFolder}/{fileName}";
                    _userManager.UpdateAsync(user);
                }


                var response = new UpdateCommandResponse
                {
                    Id = user.Id.ToString()
                };
                await _emailService.SendEmailConfirmationAsync(request.Email, emailResponse.Token);
                return Result<UpdateCommandResponse>.Succeed(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
    }
}
