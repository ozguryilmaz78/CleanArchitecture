using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.EmailConfirmation
{
    public class EmailConfirmationCommand : IRequest<Result<EmailConfirmationCommandResponse>>
    {
        public string ToEmail { get; set; }
        public string Token { get; set; }
    }
}
