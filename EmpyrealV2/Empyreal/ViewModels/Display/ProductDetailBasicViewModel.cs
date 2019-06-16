using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Display
{
    public class ProductDetailBasicViewModel
    {

        #region --- Variables ---
        
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        public int ID { get; set; }
        public int? PriceID { get; set; }
        public int? Size { get; set; }
        public int? Color { get; set; }
        public int? Quantity { get; set; }
        public int? State { get; set; }
        public int? ProductID { get; set; }

        public double? PriceText { get; set; }

        public string DisplayPrice
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.PriceText);
            }
        }
        public string DisplayOldPrice
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.PriceText * this.SalePrice);
            }
        }
        public double SalePrice
        {
            get
            {
                return 1.2;
            }

        }

        public string StateName {
            get
            {
                if (State == 0)
                    return "Đã xóa";
                else if (State == 1)
                    return "Đang bán";
                else if (State == 2)
                    return "Hết hàng";
                return string.Empty;
            }
        }

        public string ColorText { get; set; }
        public string SizeText { get; set; }

        //public List<CommentViewModel> Comments { get; set; }
        //public List<Rate> Rates { get; set; }
        //public PopupLoginViewModel PopupLoginViewModel { get; set; }

        #endregion --- Variables ---

        #region --- Constructor ---

        public ProductDetailBasicViewModel(ProductDetail productDetail)
        {
            this.ID = productDetail.Id;
            this.Size = productDetail.Size;
            this.Color = productDetail.Color;
            this.Quantity = productDetail.Quantity;
            this.PriceID = productDetail.Price;
            this.PriceText = productDetail.PriceNavigation.Price;
            this.ColorText = productDetail.ColorNavigation.Text;
            this.SizeText = productDetail.SizeNavigation.Text;
            this.ProductID = productDetail.ProductId;

            this.State = productDetail.State;
        }

        public ProductDetailBasicViewModel() {

        }

        #endregion --- Constructor ---

    }
}
