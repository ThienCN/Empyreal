using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Models.BaseModel
{
    public class History
    {
        public DateTime? CreateDate { get; set; }
        public string CreateByUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string LastModifyByUser { get; set; }

    }
}
