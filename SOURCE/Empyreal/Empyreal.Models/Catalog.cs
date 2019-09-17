using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Catalog
    {
        public Catalog()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
