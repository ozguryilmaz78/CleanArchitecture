using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Register
{
    public partial class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterCommandResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtProvider _jwtProvider;

        public RegisterCommandHandler(
            UserManager<AppUser> userManager,
            IJwtProvider jwtProvider,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AppUser? user = await _userManager.Users
                            .FirstOrDefaultAsync(p =>
                            p.UserName == request.UserName ||
                            p.Email == request.Email,
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
                    Email = request.Email,
                };
                var emailResponse = await _jwtProvider.CreateEmailConfirmationToken(user);
                user.EmailConfirmationCode = emailResponse.Token;
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Result<RegisterCommandResponse>.Failure(500, $"Kayıt başarısız: {errors}");
                }

                var response = new RegisterCommandResponse
                {
                    Id = user.Id.ToString()
                };
                await _emailService.SendEmailConfirmationAsync(request.Email, emailResponse.Token);
                return Result<RegisterCommandResponse>.Succeed(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
    }
}
