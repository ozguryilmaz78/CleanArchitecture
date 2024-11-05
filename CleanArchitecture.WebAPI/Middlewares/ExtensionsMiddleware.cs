using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.Role;

namespace CleanArchitecture.WebAPI.Middlewares
{
    public static class ExtensionsMiddleware
    {
        public static async void CreateFirstUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
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
                    User user = new()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        FirstName = "Admin",
                        LastName = "AdminSurname",
                        EmailConfirmed = true,
                    };

                    userManager.CreateAsync(user, "Oy12345**").Wait();
                    User? adminUser = await userManager.FindByNameAsync("Admin");
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }

            }
        }
    }
}
