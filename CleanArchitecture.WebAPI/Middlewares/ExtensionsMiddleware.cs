using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.AppRole;

namespace CleanArchitecture.WebAPI.Middlewares
{
    public class ExtensionsMiddleware
    {
        public static async void CreateFirstUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<RoleDomain>>();

                if (!roleManager.Roles.Any(p => p.Name == "Admin"))
                {
                    RoleDomain role = new()
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        Description = "Yönetici",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    roleManager.CreateAsync(role).Wait();
                }

                if (!userManager.Users.Any(p => p.UserName == "admin"))
                {
                    AppUser user = new()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        FirstName = "Admin",
                        LastName = "AdminSurname",
                        FullName = "Admin AdminSurname",
                        EmailConfirmed = true,
                    };

                    userManager.CreateAsync(user, "Oy12345**").Wait();
                    AppUser? adminUser = await userManager.FindByNameAsync("Admin");
                    if (adminUser != null)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

            }
        }
    }
}
