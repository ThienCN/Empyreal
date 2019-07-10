using Empyreal.ViewModels.Display;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.History
{
    public class PagedHistoryViewModel
    {
        public PagedList<HistoryBasicViewModel> PagedList { get; set; }
        public int DetailID { get; set; }
        public PagedHistoryViewModel(List<HistoryBasicViewModel> history, int page, int pageSize)
        {
            this.PagedList = new PagedList<HistoryBasicViewModel>(history.AsQueryable(), page, pageSize);

        }
        public PagedHistoryViewModel()
        {
        }
    }
}
