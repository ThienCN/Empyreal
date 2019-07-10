using Empyreal.Controllers;
using Empyreal.Models;
using Empyreal.ViewModels.Base;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Manager
{
    /// <summary>
    /// ViewModel: Dùng để thao tác với Màn hình Chi tiết, Cập nhật sản phẩm
    /// </summary>
    public class ProductUpdateViewModel: ManagerBaseViewModel
    {
        #region --- Variables ---

        /// <summary>
        /// Mã người tạo
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// HoTen Người tạo
        /// </summary>
        public string UserName { get; set; }
        public int ProductId { get; set; }

        /// <summary>
        /// Input: Tên sản phảm
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Input: Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Input: Danh mục
        /// </summary>
        public int Catalog { get; set; }

        /// <summary>
        /// Input: Mã chi tiết sản phẩm 
        /// </summary>
        public List<int> ProductDetailId { get; set; }
        
        /// <summary>
        /// Input: Mã chi tiết sản phẩm đc XÓA
        /// </summary>
        public List<int> DeleteDetailID { get; set; }

        /// <summary>
        /// Input: Số lượng
        /// </summary>
        public List<int> Quantity { get; set; }
        
        /// <summary>
        /// Input: Giá
        /// </summary>
        public List<double> PriceText { get; set; }

        /// <summary>
        /// Input: ID của table.Price
        /// </summary>
        public List<int> PriceId { get; set; }

        /// <summary>
        /// Input: ID của table.ProductType
        /// </summary>
        public List<int> Color { get; set; }

        /// <summary>
        /// Input: ID của table.ProductType
        /// </summary>
        public List<int> Size { get; set; }

        /// <summary>
        /// Input: Url của Hình ảnh
        /// </summary>
        public List<IFormFile> Files { get; set; }

        /// <summary>
        /// Danh sách Detail
        /// </summary>
        public List<ProductDetailBasicViewModel> ProductDetails { get; set; }
        
        /// <summary>
        /// Hình ảnh
        /// </summary>
        public List<ImageBasicViewModel> Images { get; set; }
        
        /// <summary>
        /// ComboBox Kích cỡ
        /// </summary>
        public List<SelectListItem> Sizes { get; set; }
        
        /// <summary>
        /// ComboBox Màu sắc
        /// </summary>
        public List<SelectListItem> Colors { get; set; }
        
        /// <summary>
        /// ComboBox Danh mục
        /// </summary>
        public List<SelectListItem> Catalogs { get; set; }

        /// <summary>
        /// Json: Product Old use for check Save History
        /// </summary>
        public string ProductOld { get; set; } // JSON

        /// <summary>
        /// Lưu lại lịch sử chỉnh sửa sp
        /// </summary>
        public List<string> History { get; set; }

        #endregion --- Variables ---

        #region --- Constructor ---
        public ProductUpdateViewModel(Product product) {
            this.ProductName = product.Name;
            this.Catalog = product.CatalogId;
            this.Description = product.Description;
            this.IsError = true;
            this.IsHidden = true;

            // Models
            this.ProductDetails = new List<ProductDetailBasicViewModel>();
            this.Images = new List<ImageBasicViewModel>();

            //ComboBox
            this.Catalogs = new List<SelectListItem>();
            this.Sizes = new List<SelectListItem>();
            this.Colors = new List<SelectListItem>();

        }

        public ProductUpdateViewModel(){
        }
        #endregion --- Constructor ---

    }
}
