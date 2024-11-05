using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.Infrastructure.Options
{
    public sealed class JwtTokenOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        public JwtTokenOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            // Token doğrulama parametrelerini ayarlıyoruz
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Value.Issuer,
                ValidAudience = _jwtOptions.Value.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey))
            };
        }
    }
}
