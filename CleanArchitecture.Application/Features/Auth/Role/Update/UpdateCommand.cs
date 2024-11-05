using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.Update
{
    public class UpdateCommand : IRequest<Result<UpdateCommandResponse>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
