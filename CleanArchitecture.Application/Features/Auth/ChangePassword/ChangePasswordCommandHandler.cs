using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordCommandResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangePasswordCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtProvider jwtProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Result<ChangePasswordCommandResponse>.Failure(401, "Kullanıcı kimliği bulunamadı");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<ChangePasswordCommandResponse>.Failure(404, "Kullanıcı bulunamadı");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<ChangePasswordCommandResponse>.Failure(400, $"Parola değiştirme başarısız: {errors}");
            }

            var response = new ChangePasswordCommandResponse
            {
                Id = user.Id.ToString()
            };

            return Result<ChangePasswordCommandResponse>.Succeed(response);
        }
    }
}
