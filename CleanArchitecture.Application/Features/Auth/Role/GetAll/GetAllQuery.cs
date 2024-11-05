using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Role.GetAll
{
    public class GetAllQuery : IRequest<Result<List<GetAllQueryResponse>>>
    {
    }
}
