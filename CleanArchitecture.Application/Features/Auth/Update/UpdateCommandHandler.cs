using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Application.Features.Auth.Update
{
    public partial class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<UpdateCommandResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        

        public UpdateCommandHandler(
            UserManager<AppUser> userManager,
            IJwtProvider jwtProvider,
            IConfiguration configuration,
            IHostingEnvironment environment)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<Result<UpdateCommandResponse>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userImageFolder = _configuration["ManagementSettings:UserImageFolder"];
                var BackendUrl = _configuration["ManagementSettings:BackendUrl"];
                AppUser? user = await _userManager.Users
                            .FirstOrDefaultAsync(p =>
                            p.Id == request.Id,
                            cancellationToken);

                AppUser? existedUser = await _userManager.Users
                    .FirstOrDefaultAsync(p =>
                    (p.Email == request.Email ||
                    p.UserName == request.UserName) &&
                    p.Id != user.Id,
                    cancellationToken);
                if (existedUser != null)
                {
                    return (500, "Kullanıcı adı veya email sistemde kayıtlı.");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.FullName = request.FirstName + " " + request.LastName;
                user.UserName = request.UserName;
                user.Email = request.Email;

                if (request.PhotoFile != null)
                {
                    var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var fileName = $"{user.Id}_{timestamp}.jpg";
                    var filePath = Path.Combine(Path.Combine(_environment.WebRootPath, userImageFolder), fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        request.PhotoFile.CopyTo(stream);
                    }
                    user.PhotoUrl = $"{BackendUrl}/{userImageFolder}/{fileName}";
                   
                }
                await _userManager.UpdateAsync(user);
                var response = new UpdateCommandResponse
                {
                    Id = user.Id.ToString()
                };
                return Result<UpdateCommandResponse>.Succeed(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
    }
}
