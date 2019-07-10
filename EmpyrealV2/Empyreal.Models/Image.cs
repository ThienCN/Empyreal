using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Image
    {
        public Image()
        {
            this.CreateDate = DateTime.Now;
            this.LastModifyDate = DateTime.Now;

            this.State = 1;
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public int? ProductId { get; set; }
        public int? State { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateByUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string LastModifyByUser { get; set; }
        public virtual Product Product { get; set; }
    }
}
