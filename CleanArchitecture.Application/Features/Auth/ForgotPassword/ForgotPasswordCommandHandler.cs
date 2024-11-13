using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Application.Features.Auth.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordCommandResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtProvider jwtProvider,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _emailService = emailService;   
        }

        public async Task<Result<ForgotPasswordCommandResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.Users
                         .FirstOrDefaultAsync(p =>
                         p.UserName == request.EmailOrUserName ||
                         p.Email == request.EmailOrUserName,
                         cancellationToken);

            if (user is null)
            {
                return (500, "Kullanıcı bulunamadı.");
            }
            string newPassword = GenerateRandomPassword();
            var passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, newPassword);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                // Hata durumunda, hataları döndürüyoruz
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<ForgotPasswordCommandResponse>.Failure(500, $"Kayıt başarısız: {errors}");
            }
            var response = new ForgotPasswordCommandResponse
            {  UserName= user.UserName,
               Password = newPassword
            };
            await _emailService.SendEmailForgotPasswordAsync(user.Email, response);
            return Result<ForgotPasswordCommandResponse>.Succeed(response);


        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@$?_-*";
            StringBuilder res = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];
                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            return res.ToString();
        }
    }
}
