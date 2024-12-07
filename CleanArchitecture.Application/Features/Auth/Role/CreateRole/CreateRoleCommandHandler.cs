using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Domain.Entities.Auth;


namespace CleanArchitecture.Application.Features.Auth.Role.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<CreateRoleCommandResponse>>
    {
        private readonly RoleManager<AppRole> _repository;

        public CreateRoleCommandHandler(RoleManager<AppRole> repository)
        {
            _repository = repository;
        }

        public async Task<Result<CreateRoleCommandResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.RoleExistsAsync(request.Name);
            if (role == true) return Result<CreateRoleCommandResponse>.Failure("Bu isimde bir rol zaten mevcut.");
            var newRole = new AppRole
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
