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
        public string UserId { get; set; }
        public double? PriceSum { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }
        public string Address { get; set; }
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? ShippingDate { get; set; }
        public double? ShippingFee { get; set; }
        public string PaymentType { get; set; }
        public string ShippingType { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
