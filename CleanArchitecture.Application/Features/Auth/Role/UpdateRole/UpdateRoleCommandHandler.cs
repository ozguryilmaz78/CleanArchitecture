using AutoMapper;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;

namespace CleanArchitecture.Application.Features.Auth.Role.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<UpdateRoleCommandResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(RoleManager<RoleDomain> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UpdateRoleCommandResponse>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.FindByIdAsync(request.Id);
            if (role == null) return Result<UpdateRoleCommandResponse>.Failure("Rol bulunamadı.");
            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();
            role.Description = request.Description;
            await _repository.UpdateAsync(role);
            var updatedRole = _mapper.Map<UpdateRoleCommandResponse>(role);
            return Result<UpdateRoleCommandResponse>.Succeed(updatedRole);
        }
    }
}
