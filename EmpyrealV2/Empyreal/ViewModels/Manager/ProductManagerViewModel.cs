using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Manager
{
    /// <summary>
    /// ViewModel: Dùng để thao tác với Màn hình danh mục, Tìm kiếm dạng Danh sách
    /// </summary>
    public class ProductManagerViewModel
    {
        #region --- Variables ---

        public PagedList<ProductBasicViewModel> PagedProductModel { get; set; }
        public List<SelectListItem> CbbCatalog { get; set; }
        public int Catalog { get; set; }
        public string Keyword { get; set; }

        #endregion --- Variables ---

        #region --- Constructor ---

        public ProductManagerViewModel(List<ProductBasicViewModel> product ,int page, int pageSize,
            List<SelectListItem> catalogs, string keyWord, int catalogSelect)
        {
            this.PagedProductModel =new PagedList<ProductBasicViewModel>(product.AsQueryable(), page, pageSize);
            this.CbbCatalog = catalogs;
            this.Keyword = keyWord;
            this.Catalog = catalogSelect;
        }

        #endregion --- Constructor ---
    }
}
