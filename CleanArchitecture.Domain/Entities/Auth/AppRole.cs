using CleanArchitecture.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.Auth
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
        public string Discriminator { get; set; } = string.Empty;
        public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; } = new List<IdentityUserRole<Guid>>();
    }
}
