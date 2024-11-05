using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.Create
{
    public class CreateCommand : IRequest<Result<CreateCommandResponse>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
