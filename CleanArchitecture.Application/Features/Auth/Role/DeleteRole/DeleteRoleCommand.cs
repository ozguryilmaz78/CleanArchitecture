using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Result<DeleteRoleCommandResponse>>
    {
        public string Id { get; set; }
    }
}
