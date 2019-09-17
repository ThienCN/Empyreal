using System;
using System.Collections.Generic;

namespace Empyreal.ViewModels.Display
{
    public class OrderDetailViewModel
    {
        public OrderViewModel Order { get; set; }
        public List<ProductDetailBasicViewModel> ProductDetails { get; set; }
        public List<ImageBasicViewModel> Images { get; set; }
        public string DisplayDate
        {
            get
            {
                DateTime date = this.Order.DeadlineShip.GetValueOrDefault();
                string dayOfWeek = date.DayOfWeek.ToString();
                string dateShipping = date.ToString("dd/MM/yyyy");
                ChangeDay(ref dayOfWeek);
                return string.Format("{0}, {1}", dayOfWeek, dateShipping);
            }
        }

        private void ChangeDay(ref string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Monday":
                    dayOfWeek = "Thứ hai";
                    break;
                case "Tuesday":
                    dayOfWeek = "Thứ ba";
                    break;
                case "Wednesday":
                    dayOfWeek = "Thứ tư";
                    break;
                case "Thursday":
                    dayOfWeek = "Thứ năm";
                    break;
                case "Friday":
                    dayOfWeek = "Thứ sáu";
                    break;
                case "Saturday":
                    dayOfWeek = "Thứ bảy";
                    break;
                case "Sunday":
                    dayOfWeek = "Chủ nhật";
                    break;
                default:
                    dayOfWeek = "Thứ hai";
                    break;
            }
        }

        public OrderDetailViewModel(OrderViewModel order, List<ProductDetailBasicViewModel> productDetails,
                                        List<ImageBasicViewModel> images)
        {
            this.Order = order;
            this.ProductDetails = productDetails;
            this.Images = images;
        }

        public OrderDetailViewModel() { }
    }
}
