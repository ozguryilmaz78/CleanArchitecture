using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.Auth
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? EmailConfirmationCode { get; set; }
        public string? FullName { get; set; }  // Setter eklendi
        public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; } = new List<IdentityUserRole<Guid>>();
    }
}
