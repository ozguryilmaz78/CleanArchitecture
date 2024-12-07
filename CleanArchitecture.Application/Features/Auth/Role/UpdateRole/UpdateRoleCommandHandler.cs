using AutoMapper;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Domain.Entities.Auth;

namespace CleanArchitecture.Application.Features.Auth.Role.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<UpdateRoleCommandResponse>>
    {
        private readonly RoleManager<AppRole> _repository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(RoleManager<AppRole> repository, IMapper mapper)
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
