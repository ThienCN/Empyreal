using System;
using System.ComponentModel.DataAnnotations;

namespace Empyreal.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên"), MaxLength(256, ErrorMessage = "Họ tên không quá 100 ký tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email tối đa 100 ký tự"), MaxLength(256, ErrorMessage = "Email tối đa 100 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tháng sinh")]
        public string MonthOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn năm sinh")]
        public string YearOfBirth { get; set; }

        public string Sex { get; set; }
    }
}
