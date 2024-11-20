using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.UpdateRole
{
    public class UpdateRoleCommand : IRequest<Result<UpdateRoleCommandResponse>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
