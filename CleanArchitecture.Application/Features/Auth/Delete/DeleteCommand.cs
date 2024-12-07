using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Delete
{
    public class DeleteCommand : IRequest<Result<DeleteCommandResponse>>
    {
        public string Id { get; set; }
    }
}
