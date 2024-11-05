using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Role.Assign
{
    public class AssignCommand : IRequest<Result<AssignCommandResponse>>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
