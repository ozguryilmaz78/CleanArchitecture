using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Delete
{
    public partial class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<DeleteCommandResponse>>
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteCommandHandler(
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<DeleteCommandResponse>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AppUser? user = await _userManager.Users
                            .FirstOrDefaultAsync(p =>
                            p.Id == request.Id, cancellationToken);

                if (user == null)
                {
                    return (500, "Kullanıcı sistemde kayıtlı değil.");
                }

                user.IsDeleted = true;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Result<DeleteCommandResponse>.Failure(500, $"Silme başarısız: {errors}");
                }

                var response = new DeleteCommandResponse
                {
                    Id = user.Id.ToString()
                };
                return Result<DeleteCommandResponse>.Succeed(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
    }
}
