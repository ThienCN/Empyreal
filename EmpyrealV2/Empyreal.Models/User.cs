using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empyreal.Models
{
    [Table("AspNetUsers")]
    public partial class User: IdentityUser
    {
        public User()
        {
            //AspNetUserClaims = new HashSet<AspNetUserClaims>();
            //AspNetUserLogins = new HashSet<AspNetUserLogins>();
            //AspNetUserRoles = new HashSet<AspNetUserRoles>();
            //AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Product = new HashSet<Product>();
            History = new HashSet<History>();

        }

        public string HoTen { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public int? State { get; set; }
        public DateTime CreateDate { get; set; }

        //public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        //public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        //public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        //public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<History> History { get; set; }

    }
}
