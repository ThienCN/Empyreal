using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }
    }
}
