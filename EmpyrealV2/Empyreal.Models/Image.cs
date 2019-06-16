using Empyreal.Models.BaseModel;
using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Image: History
    {
        public Image()
        {
            this.CreateDate = (DateTime)DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            this.State = 1;
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public int? ProductId { get; set; }
        public int? State { get; set; }

        public virtual Product Product { get; set; }
    }
}
