using System;
using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int? CommentId { get; set; }
        public string UserId { get; set; }
        public string Contents { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }
    }
}
