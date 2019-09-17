using Empyreal.ViewModels.Base;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Empyreal.ViewModels.Manager
{
    public class OrderUpdateViewModel: ManagerBaseViewModel
    {

        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        public int OrderID { get; set; }
        public string Shipper { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public int State { get; set; }
        public List<SelectListItem> CbbShipper { get; set; }
        public DateTime? DeadlineShip { get; set; }      
        public DateTime? ShippingDate { get; set; }
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// use for Order
        /// </summary>
        public double? SumPrice { get; set; }
        public double? ShippingFee { get; set; }
        public double? FinalPrice { get; set; }

        /// <summary>
        /// use for Order
        /// </summary>
        public string DisplaySumPrice
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.SumPrice);
            }
        }
        public string DisplayShippingFee
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.ShippingFee);
            }
        }
        public string DisplayFinalPrice
        {
            get
            {
                return String.Format(cul, "{0:c0}", this.FinalPrice);
            }
        }
        public List<ProductDetailBasicViewModel> ProductDetail { get; set; }

    }
}
