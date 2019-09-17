using Empyreal.ViewModels.Display;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Search
{
    public class PagedSearchViewModel
    {
        public PagedList<ProductBasicViewModel> PagedProductModel { get; set; }
        public PagedSearchViewModel(IEnumerable<ProductBasicViewModel> products, int page, int pageSize)
        {
            this.PagedProductModel = new PagedList<ProductBasicViewModel>(products.AsQueryable(), page, pageSize);

        }
        public PagedSearchViewModel()
        {
        }
    }
}
