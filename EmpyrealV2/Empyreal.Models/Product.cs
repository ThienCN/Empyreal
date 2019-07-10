using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Product
    {
        public Product()
        {
            this.CreateDate = DateTime.Now;
            this.State = 1;

            Images = new HashSet<Image>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? State { get; set; }
        public string UserId { get; set; }
        public int CatalogId { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateByUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string LastModifyByUser { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
