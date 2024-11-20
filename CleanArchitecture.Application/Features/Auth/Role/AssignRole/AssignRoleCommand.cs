using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Role.AssignRole
{
    public class AssignRoleCommand : IRequest<Result<AssignRoleCommandResponse>>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
