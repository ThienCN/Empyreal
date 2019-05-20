using System.ComponentModel.DataAnnotations;

namespace Empyreal.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email hoặc sđt")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
