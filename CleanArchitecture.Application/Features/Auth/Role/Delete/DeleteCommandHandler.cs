using AutoMapper;
using CleanArchitecture.Application.Features.Auth.EmailVerify;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;

namespace CleanArchitecture.Application.Features.Auth.Role.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<DeleteCommandResponse>>
    {
        private readonly RoleManager<RoleDomain> _repository;

        public DeleteCommandHandler(RoleManager<RoleDomain> repository)
        {
            _repository = repository;
        }
        public async Task<Result<DeleteCommandResponse>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.FindByIdAsync(request.Id);
            if (role == null) return Result<DeleteCommandResponse>.Failure("Rol bulunamadı.");
            await _repository.DeleteAsync(role);
            var response = new DeleteCommandResponse
            {
                Message = "Rol başarıyla silindi."
            };
            return Result<DeleteCommandResponse>.Succeed(response);
        }
    }
}
