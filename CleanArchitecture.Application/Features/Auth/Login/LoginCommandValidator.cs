using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Login
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.EmailOrUserName)
                .MinimumLength(3)
                .WithMessage("Kullanıcı adı ya da mail bilgisi en az 3 karakter olmalıdır");
            RuleFor(p => p.Password)
                .MinimumLength(1)
                .WithMessage("Şifre en az 1 karakter olmalıdır");
        }
    }
}
