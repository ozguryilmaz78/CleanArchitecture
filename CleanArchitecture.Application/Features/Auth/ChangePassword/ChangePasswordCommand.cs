using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result<ChangePasswordCommandResponse>>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
