using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            this.CreateDate = DateTime.Now;
            this.State = 1;

            CartDetails = new HashSet<CartDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductPrices = new HashSet<ProductPrice>();

        }
        
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Price { get; set; }
        public int? Size { get; set; }
        public int? Color { get; set; }
        public int? Quantity { get; set; }
        public int State { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateByUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string LastModifyByUser { get; set; }
        public virtual ProductType ColorNavigation { get; set; }
        public virtual ProductPrice PriceNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductType SizeNavigation { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }

    }
}
