using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Models
{
    public class ProductReview
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int View { get; set; }
        public int Saled { get; set; }
        public virtual Product Product { get; set; }

    }
}
