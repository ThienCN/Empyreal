using Empyreal.ViewModels.Display;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Manager
{
    public class OrderManagerViewModel
    {
        public PagedList<OrderViewModel> PagedOrderModel { get; set; }

        public string Keyword { get; set; }

    }
}
