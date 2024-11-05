using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.EmailVerify
{
    public class EmailVerifyCommand:IRequest<Result<EmailVerifyCommandResponse>>
    {
        public string Token { get; set; }
    }
}
