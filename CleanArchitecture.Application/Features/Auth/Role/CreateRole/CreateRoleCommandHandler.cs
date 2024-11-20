using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;


namespace CleanArchitecture.Application.Features.Auth.Role.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<CreateRoleCommandResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;

        public CreateRoleCommandHandler(RoleManager<RoleDomain> repository)
        {
            _repository = repository;
        }

        public async Task<Result<CreateRoleCommandResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.RoleExistsAsync(request.Name);
            if (role == true) return Result<CreateRoleCommandResponse>.Failure("Bu isimde bir rol zaten mevcut.");
            var newRole = new RoleDomain
            {
                Name = request.Name,
                NormalizedName = request.Name.ToUpper(),
                Description = request.Description,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await _repository.CreateAsync(newRole);
            if (!result.Succeeded)
            {
                return Result<CreateRoleCommandResponse>.Failure(500, $"Kayıt başarısız.");
            }

            return Result<CreateRoleCommandResponse>.Succeed(new CreateRoleCommandResponse
            {
                Name = newRole.Name,
                NormalizedName = newRole.Name.ToUpper(),
                Description = newRole.Description,
                ConcurrencyStamp = newRole.ConcurrencyStamp
            });
        }
    }
}
