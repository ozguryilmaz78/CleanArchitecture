using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Result<List<GetAllQueryResponse>>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(
            IRepository<AppUser> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllQueryResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllAsync();
            if (entity == null) return Result<List<GetAllQueryResponse>>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<List<GetAllQueryResponse>>(entity);
            return Result<List<GetAllQueryResponse>>.Succeed(response);
        }
    }

}
