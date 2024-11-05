using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.Role;


namespace CleanArchitecture.Application.Features.Auth.Role.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;

        public CreateCommandHandler(RoleManager<RoleDomain> repository)
        {
            _repository = repository;
        }

        public async Task<Result<CreateCommandResponse>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.RoleExistsAsync(request.Name);
            if (role == true) return Result<CreateCommandResponse>.Failure("Bu isimde bir rol zaten mevcut.");
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
                return Result<CreateCommandResponse>.Failure(500, $"Kayıt başarısız.");
            }

            return Result<CreateCommandResponse>.Succeed(new CreateCommandResponse
            {
                Name = newRole.Name,
                NormalizedName = newRole.Name.ToUpper(),
                Description = newRole.Description,
                ConcurrencyStamp = newRole.ConcurrencyStamp
            });
        }
    }
}
