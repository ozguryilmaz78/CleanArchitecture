using AutoMapper;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;

namespace CleanArchitecture.Application.Features.Auth.Role.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<UpdateCommandResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(RoleManager<RoleDomain> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UpdateCommandResponse>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.FindByIdAsync(request.Id);
            if (role == null) return Result<UpdateCommandResponse>.Failure("Rol bulunamadı.");
            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();
            role.Description = request.Description;
            await _repository.UpdateAsync(role);
            var updatedRole = _mapper.Map<UpdateCommandResponse>(role);
            return Result<UpdateCommandResponse>.Succeed(updatedRole);
        }
    }
}
