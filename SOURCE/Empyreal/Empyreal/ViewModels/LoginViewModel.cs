using Empyreal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels
{
    public class LoginViewModel
    {
        //private readonly SignInManager<User> _signInManager;

        //public LoginViewModel(SignInManager<User> signInManager,
        //    IList<AuthenticationScheme> ExternalLogins)
        //{
        //    _signInManager = signInManager;
        //    this.ExternalLogins = ExternalLogins;
        //}

        [Required(ErrorMessage = "Vui lòng nhập email hoặc sđt")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu"), DataType(DataType.Password)]
        public string Password { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    //if (!string.IsNullOrEmpty(ErrorMessage))
        //    //{
        //    //    ModelState.AddModelError(string.Empty, ErrorMessage);
        //    //}

        //    //returnUrl = returnUrl ?? Url.Content("~/");

        //    // Clear the existing external cookie to ensure a clean login process
        //    //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    //ReturnUrl = returnUrl;
        //}
    }
}
