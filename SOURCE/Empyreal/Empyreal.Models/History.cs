using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Models
{
    /// <summary>
    /// Lưu lại lịch sử tác động db
    /// </summary>
    public partial class History
    {
        public int Id { get; set; }
        public string Table { get; set; }
        public int Detail { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateByUser { get; set; }

        public virtual User CreateByUserNavigation { get; set; }
    }
}
