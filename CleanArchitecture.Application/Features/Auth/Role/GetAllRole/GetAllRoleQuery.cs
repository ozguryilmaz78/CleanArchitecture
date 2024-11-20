using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.GetAllRole
{
    public class GetAllRoleQuery : IRequest<Result<List<GetAllRoleQueryResponse>>>
    {
    }
}
