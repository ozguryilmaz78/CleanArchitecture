using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<Result<ForgotPasswordCommandResponse>>
    {
        public string EmailOrUserName { get; set; }
    }
}
