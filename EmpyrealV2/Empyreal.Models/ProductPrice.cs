using Empyreal.Models.BaseModel;
using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class ProductPrice: History
    {
        public ProductPrice()
        {
            this.Id = 0;
            this.CreateDate = DateTime.Now;
            this.State = 1;

            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public int? ProductDetailId { get; set; }
        public double? Price { get; set; }
        public int? State { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ProductDetail ProductDetailNavigation { get; set; }

    }
}
