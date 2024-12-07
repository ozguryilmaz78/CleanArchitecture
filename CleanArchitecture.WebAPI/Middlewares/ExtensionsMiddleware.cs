using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;


namespace CleanArchitecture.WebAPI.Middlewares
{
    public class ExtensionsMiddleware
    {
        public static async void CreateFirstUser(WebApplication app)
        {
       
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                if (!roleManager.Roles.Any(p => p.Name == "Admin"))
                {
                    var roles = new List<AppRole>
                    {
                        new AppRole
                        {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        Description = "Yönetici",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                        },
                        new AppRole
                        {
                        Name = "User",
                        NormalizedName = "USER",
                        Description = "Kullanıcı",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                        }
                    };

                    foreach (var role in roles)
                    {
                        roleManager.CreateAsync(role).Wait();
                    }
                }

                if (!userManager.Users.Any(p => p.UserName == "admin"))
                {
                    AppUser user = new()
                    {
                        UserName = "admin",
                        Email = "admin@identity.com",
                        FirstName = "Admin",
                        LastName = "Yönetici",
                        FullName = "Admin Yönetici",
                        EmailConfirmed = true,
                    };

                    var result = await userManager.CreateAsync(user, "Admin2024*");
                    if (result.Succeeded)
                    {
                        AppUser? adminUser = await userManager.FindByNameAsync("admin");
                        if (adminUser != null)
                        {
                            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                        }
                    }
                }

            }
        }

    }
}
