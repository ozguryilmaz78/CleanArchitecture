using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Login
{
    public class LoginCommand : IRequest<Result<LoginCommandResponse>>
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
}
