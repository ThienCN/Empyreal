using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            ProductDetailColorNavigation = new HashSet<ProductDetail>();
            ProductDetailSizeNavigation = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public int? State { get; set; }

        public virtual ICollection<ProductDetail> ProductDetailColorNavigation { get; set; }
        public virtual ICollection<ProductDetail> ProductDetailSizeNavigation { get; set; }
    }
}
