using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IRepository<AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<GetByIdQueryResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return Result<GetByIdQueryResponse>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<GetByIdQueryResponse>(entity);
            return Result<GetByIdQueryResponse>.Succeed(response);
        }
    }
}
