using AutoMapper;
using CleanArchitecture.Application.Services;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Role.GetAllRole
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, Result<List<GetAllRoleQueryResponse>>>
    {
        private readonly RoleManager<RoleDomain> _repository;
        private readonly IMapper _mapper;

        public GetAllRoleQueryHandler(RoleManager<RoleDomain> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllRoleQueryResponse>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.Roles.ToListAsync(cancellationToken);
            if (roles == null) return Result<List<GetAllRoleQueryResponse>>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<List<GetAllRoleQueryResponse>>(roles);
            return Result<List<GetAllRoleQueryResponse>>.Succeed(response);
        }
    }
}
