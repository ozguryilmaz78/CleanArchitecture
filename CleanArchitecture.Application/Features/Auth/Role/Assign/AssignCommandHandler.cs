using AutoMapper;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Role.Assign
{
    public class AssignCommandHandler : IRequestHandler<AssignCommand, Result<AssignCommandResponse>>
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<RoleDomain> _roleManager;
        private IMapper _mapper;

        public AssignCommandHandler(UserManager<AppUser> userManager, RoleManager<RoleDomain> roleManager, IRepository<AppUserRole> userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Result<AssignCommandResponse>> Handle(AssignCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return Result<AssignCommandResponse>.Failure("Kullanıcı bulunamadı.");

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return Result<AssignCommandResponse>.Failure("Rol bulunamadı.");

            // Kullanıcının mevcut rollerini kontrol etmek için rolu doğrudan UserManager'dan kontrol edin
            if (await _userManager.IsInRoleAsync(user, role.Name))
                return Result<AssignCommandResponse>.Failure("Bu rol kullanıcıya önceden tanımlanmıştır.");

            // Role ekle
            var addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addToRoleResult.Succeeded)
                return Result<AssignCommandResponse>.Failure("Rol ataması başarısız oldu.");

            // Kullanıcı güncellendikten sonra yanıtı map'lemek
            var response = _mapper.Map<AssignCommandResponse>(user);
            return Result<AssignCommandResponse>.Succeed(response);
        }
    }
}
