using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.GetAll
{
    public class GetAllQuery : IRequest<Result<List<GetAllQueryResponse>>>
    {
    }
}
