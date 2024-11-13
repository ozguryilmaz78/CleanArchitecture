using AutoMapper;
using CleanArchitecture.Application.Services;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Role.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(RoleManager<RoleDomain> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<GetByIdQueryResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.FindByIdAsync(request.Id);
            if (role == null) return Result<GetByIdQueryResponse>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<GetByIdQueryResponse>(role);
            return Result<GetByIdQueryResponse>.Succeed(response);
        }
    }
}
