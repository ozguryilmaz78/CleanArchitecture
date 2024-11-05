using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(User user);
        Task<EmailConfirmationCommandResponse> CreateEmailConfirmationToken(User user);
    }
}
