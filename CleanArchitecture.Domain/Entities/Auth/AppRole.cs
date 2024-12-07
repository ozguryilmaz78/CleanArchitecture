using CleanArchitecture.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.Auth
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
