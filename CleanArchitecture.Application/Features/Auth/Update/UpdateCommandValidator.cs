using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Update
{
        public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
        {
            public UpdateCommandValidator()
            {
                RuleFor(p => p.FirstName)
                    .MinimumLength(1)
                    .WithMessage("İsim en az 1 karakter olmalıdır");
                RuleFor(p => p.LastName)
                    .MinimumLength(1)
                    .WithMessage("Soyadı en az 1 karakter olmalıdır");
                RuleFor(p => p.UserName)
                    .MinimumLength(1)
                    .WithMessage("Kullanıcı adı en az 1 karakter olmalıdır");
                RuleFor(p => p.Email)
                    .MinimumLength(1)
                    .WithMessage("Eposta en az 1 karakter olmalıdır");
            }
        }
}
