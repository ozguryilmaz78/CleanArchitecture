using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CleanArchitecture.Domain.Entities.Auth
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? EmailConfirmationCode { get; set; }
        public string? FullName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string PhotoUrl { get; set; } = "https://localhost:5000/img/user/defaultUser.jpg";
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        [NotMapped]
        public string UserRole { get; set; }
       
    }
}
