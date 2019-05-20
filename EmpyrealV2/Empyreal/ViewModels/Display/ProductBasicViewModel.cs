using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Display
{
    public class ProductBasicViewModel
    {
        #region --- Variables ---

        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        public int Id { get; set; }
        public string Name { get; set; }
        public int? State { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CatalogName { get; set; }
        public DateTime? LastDate { get; set; }
        public string Description { get; set; }
        public double? PriceProduct { get; set; }
        public string PriceProductText
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.PriceProduct);
            }
        }
        public string OldPriceProductText
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.PriceProduct * this.SalePrice);
            }
        }
        public double SalePrice
        {
            get
            {
                return 0.8;
            }

        }
        //public int? CartDetailID { get; set; }
        public List<ImageBasicViewModel> Images { get; set; }
        public List<ProductDetailBasicViewModel> Details { get; set; }

        #endregion --- Variables ---

        #region --- Constructor ---

        public ProductBasicViewModel()
        {
        }

        #endregion --- Constructor ---

    }
}
