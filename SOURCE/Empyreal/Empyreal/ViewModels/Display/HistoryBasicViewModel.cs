using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Display
{
    public class HistoryBasicViewModel
    {
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string DetailID { get; set; }

        public DateTime CreateDate { get; set; }
        
    }
}
