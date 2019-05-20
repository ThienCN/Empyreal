using Empyreal.Models.BaseModel;
using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Product: History
    {
        public Product()
        {
            this.CreateDate = (DateTime)DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
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

        public virtual Catalog Catalog { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
