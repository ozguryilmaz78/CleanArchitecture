using AutoMapper;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.Role;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Role.Assign
{
    public class AssignCommandHandler : IRequestHandler<AssignCommand, Result<AssignCommandResponse>>
    {
        private UserManager<User> _userManager;
        private RoleManager<RoleDomain> _roleManager;
        private IRepository<UserRole> _userRepository;
        private IMapper _mapper;

        public AssignCommandHandler(UserManager<User> userManager, RoleManager<RoleDomain> roleManager, IRepository<UserRole> userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<AssignCommandResponse>> Handle(AssignCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return Result<AssignCommandResponse>.Failure("Kullanıcı bulunamadı.");
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role == null) return Result<AssignCommandResponse>.Failure("Role bulunamadı.");
            var userRole = await _userRepository.GetByExpressionAsync(x=> x.RoleId == request.RoleId && x.UserId == request.UserId);
            if (userRole != null) return Result<AssignCommandResponse>.Failure($"Kullanıcıya ait rol bulunmaktadır.");
            await _userManager.AddToRoleAsync(user, role.Name);
            var response = _mapper.Map<AssignCommandResponse>(user);
            return Result<AssignCommandResponse>.Succeed(response);
        }
    }
}
