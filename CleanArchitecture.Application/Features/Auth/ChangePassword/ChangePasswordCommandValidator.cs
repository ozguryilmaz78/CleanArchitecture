using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(p => p.NewPassword)
                .MinimumLength(8)
                .WithMessage("Şifre minimum 8 karakter olmalıdır");
        }
    }
}
