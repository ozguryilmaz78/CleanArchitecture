using AutoMapper;
using CleanArchitecture.Application.Features.Auth.EmailVerify;
using CleanArchitecture.Application.Features.Auth.Role.AssignRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;


namespace CleanArchitecture.Application.Features.Auth.Role.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<DeleteRoleCommandResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;
        private readonly UserManager<AppUser> _userManager;

        public DeleteRoleCommandHandler(RoleManager<RoleDomain> repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<Result<DeleteRoleCommandResponse>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.FindByIdAsync(request.Id);
            if (role == null) return Result<DeleteRoleCommandResponse>.Failure("Rol bulunamadı.");
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole != null && usersInRole.Any())
                return Result<DeleteRoleCommandResponse>.Failure("Rolün tanımlandığı kullanıcılar bulunduğundan rolü silemezsiniz.");
            await _repository.DeleteAsync(role);
            var response = new DeleteRoleCommandResponse
            {
                Message = "Rol başarıyla silindi."
            };
            return Result<DeleteRoleCommandResponse>.Succeed(response);
        }
    }
}
