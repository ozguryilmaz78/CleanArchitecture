using CleanArchitecture.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Application.Features.Auth.GetById
{
    public class GetByIdQueryResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => string.Join(" ", FirstName, LastName);
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? EmailConfirmationCode { get; set; }
        public virtual ICollection<AppRole> Roles { get; set; }
        public virtual string UserRole
        {
            get
            {
                if (Roles != null && Roles.Any())
                {
                    return string.Join(", ", Roles.Select(role => role.Description));
                }
                return null;
            }
        }
        public virtual string UserRoleId
        {
            get
            {
                if (Roles != null && Roles.Any())
                {
                    return string.Join(", ", Roles.Select(role => role.Id));
                }
                return null;
            }
        }
    }
}
