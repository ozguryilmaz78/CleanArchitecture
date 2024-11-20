using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.CreateRole
{
    public class CreateRoleCommand : IRequest<Result<CreateRoleCommandResponse>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
