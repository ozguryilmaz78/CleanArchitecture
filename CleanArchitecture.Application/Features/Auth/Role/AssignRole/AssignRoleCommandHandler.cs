using AutoMapper;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Role.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result<AssignRoleCommandResponse>>
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IMapper _mapper;

        public AssignRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IRepository<AppUserRole> userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Result<AssignRoleCommandResponse>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return Result<AssignRoleCommandResponse>.Failure("Kullanıcı bulunamadı.");

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return Result<AssignRoleCommandResponse>.Failure("Rol bulunamadı.");

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            // Role ekle
            var addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addToRoleResult.Succeeded)
                return Result<AssignRoleCommandResponse>.Failure("Rol ataması başarısız oldu.");

            // Kullanıcı güncellendikten sonra yanıtı map'lemek
            var response = _mapper.Map<AssignRoleCommandResponse>(user);
            return Result<AssignRoleCommandResponse>.Succeed(response);
        }
    }
}
