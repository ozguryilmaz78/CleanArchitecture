using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Domain.Entities.State;

namespace CleanArchitecture.Application.Services
{
    public interface IDapperRepository
    {
        //User
        Task<IEnumerable<GetAllQueryResponse>> GetAllWithUserRolesAsync(GetAllQuery request);
        Task<GetByIdQueryResponse> GetByIdWithUserRolesAsync(string id);
    }
}
