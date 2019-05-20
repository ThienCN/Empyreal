using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public double? PriceSum { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
