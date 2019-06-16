using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Empyreal.ViewModels.Display
{
    public class ShippingViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên"), MinLength(6, ErrorMessage = "Họ tên ít nhất 6 ký tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại"),
            MinLength(10, ErrorMessage = "Số điện thoại phải có 10 ký tự"),
            MaxLength(10, ErrorMessage = "Số điện thoại phải có 10 ký tự")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn tỉnh")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn quận")]
        public string District { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn phường")]
        public string Ward { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }
        public string AddressType { get; set; }

        public List<DistrictViewModel> Districts { get; set; }
        public List<ProvinceViewModel> Provinces { get; set; }
        public List<WardViewModel> Wards { get; set; }
        public OrderViewModel Order { get; set; }

        public ShippingViewModel()
        {
            Districts = new List<DistrictViewModel>();
            Provinces = new List<ProvinceViewModel>();
            Wards = new List<WardViewModel>();
        }
    }
}
