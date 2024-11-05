using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.Auth
{
    public class UserRole: IdentityUserRole<Guid>
    {
        public string UserId {  get; set; }
        public string RoleId { get; set; }
    }
}
