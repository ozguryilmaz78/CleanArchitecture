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
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
