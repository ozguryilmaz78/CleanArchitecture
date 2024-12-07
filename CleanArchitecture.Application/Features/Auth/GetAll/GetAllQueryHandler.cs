using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Result<List<GetAllQueryResponse>>>
    {
        private readonly IDapperRepository _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(
            IDapperRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllQueryResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllWithUserRolesAsync(request);

            if (entity == null) return Result<List<GetAllQueryResponse>>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<List<GetAllQueryResponse>>(entity);
            return Result<List<GetAllQueryResponse>>.Succeed(response);
        }
    }

}
