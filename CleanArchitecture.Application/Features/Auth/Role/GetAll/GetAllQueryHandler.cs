using AutoMapper;
using CleanArchitecture.Application.Services;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Role.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Result<List<GetAllQueryResponse>>>
    {
        private readonly RoleManager<RoleDomain> _repository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(RoleManager<RoleDomain> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllQueryResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.Roles.ToListAsync(cancellationToken);
            if (roles == null) return Result<List<GetAllQueryResponse>>.Failure("Kayıt bulunamadı.");
            var response = _mapper.Map<List<GetAllQueryResponse>>(roles);
            return Result<List<GetAllQueryResponse>>.Succeed(response);
        }
    }
}
