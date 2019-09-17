using Empyreal.ViewModels.Base;
using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Empyreal.ViewModels.Display
{
    public class PaymentViewModel : ManagerBaseViewModel
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        public int OrderId { get; set; }
        public string ShippingType { get; set; }
        public string PaymentType { get; set; }
        public string DateText { get; set; }
        public bool IsRelative { get; set; }
        [RequiredIf("IsRelative == true", ErrorMessage = "Vui lòng nhập họ tên"), MinLength(6, ErrorMessage = "Họ tên ít nhất 6 ký tự")]
        public string RelativeName { get; set; }
        [RequiredIf("IsRelative == true", ErrorMessage = "Vui lòng nhập số điện thoại"),
            MinLength(10, ErrorMessage = "Số điện thoại phải có 10 ký tự"),
            MaxLength(10, ErrorMessage = "Số điện thoại phải có 10 ký tự")]
        public string RelativePhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsPaymentSuccess { get; set; }
        public double TempPrice { get; set; }
        public double ShippingFee { get; set; }
        public double FinalPrice
        {
            get
            {
                return TempPrice + ShippingFee;
            }
        }

        public string DisplayTempPrice
        {
            get
            {
                return string.Format(cul, "{0:c0}", this.TempPrice);
            }
        }

        public string DisplayShippingFee
        {
            get
            {
                return string.Format(cul, "{0:c0}", this.ShippingFee);
            }
        }

        public string DisplayFinalPrice
        {
            get
            {
                return string.Format(cul, "{0:c0}", this.FinalPrice);
            }
        }

        public ShippingViewModel Shipping { get; set; }
        public CartViewModel Products { get; set; }
    }
}
