using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.Infrastructure.Services;
using RoleDomain = CleanArchitecture.Domain.Entities.Auth.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Data;
using System.Reflection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server bağlantı dizesini yapılandırma
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            services.AddScoped<IDbConnection>(provider =>
            {
                var connectionString = configuration.GetConnectionString("SqlServer");
                return new SqlConnection(connectionString);
            });

            // UnitOfWork kaydını yapılandırma
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            // Identity ayarlarını yapılandırma
            services.AddIdentity<User, RoleDomain>(cfr =>
            {
                cfr.Password.RequiredLength = 8;
                cfr.Password.RequireNonAlphanumeric = true;
                cfr.Password.RequireUppercase = true;
                cfr.Password.RequireLowercase = true;
                cfr.Password.RequireDigit = false;
                cfr.SignIn.RequireConfirmedEmail = true;
                cfr.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                cfr.Lockout.MaxFailedAccessAttempts = 3;
                cfr.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // JWT ayarlarını yapılandırma
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtTokenOptionsSetup>();
            services.AddAuthentication()
                .AddJwtBearer();
            services.AddAuthorizationBuilder();

            // Email servisini kaydetme
            services.AddTransient<IEmailService, EmailService>();

            // Dependency Injection ile servisleri tarama ve kaydetme
            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            return services;
        }
    }
}
