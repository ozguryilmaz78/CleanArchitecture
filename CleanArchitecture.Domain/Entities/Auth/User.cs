using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? EmailConfirmationCode { get; set; }
        public string FullName
        {
            get
            {
               return string.Join(" ", FirstName, LastName);
            }
        }
    }
}
