using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetail = new HashSet<CartDetail>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public int? State { get; set; }

        public virtual ICollection<CartDetail> CartDetail { get; set; }
    }
}
