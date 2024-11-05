using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.Delete
{
    public class DeleteCommand : IRequest<Result<DeleteCommandResponse>>
    {
        public string Id { get; set; }
    }
}
