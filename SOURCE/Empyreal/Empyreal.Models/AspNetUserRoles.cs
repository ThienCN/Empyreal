using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual User User { get; set; }
    }
}
