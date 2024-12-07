using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Role.GetByIdRole
{
    public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, Result<GetByIdRoleQueryResponse>>
    {
        private readonly RoleManager<AppRole> _repository;
        private readonly IMapper _mapper;

        public GetByIdRoleQueryHandler(RoleManager<AppRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<GetByIdRoleQueryResponse>> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.FindByIdAsync(request.Id);
            if (role == null) return Result<GetByIdRoleQueryResponse>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<GetByIdRoleQueryResponse>(role);
            return Result<GetByIdRoleQueryResponse>.Succeed(response);
        }
    }
}
