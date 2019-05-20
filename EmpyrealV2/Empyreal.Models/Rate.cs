using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Rate
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }
        public int? Star { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Tilte { get; set; }
        public int? State { get; set; }
    }
}
