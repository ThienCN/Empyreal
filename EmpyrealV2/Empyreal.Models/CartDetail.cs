using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class CartDetail
    {
        public int Id { get; set; }
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? State { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}
