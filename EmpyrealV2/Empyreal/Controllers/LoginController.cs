using System;
using System.Threading.Tasks;
using Empyreal.Models;
using Empyreal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Empyreal.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        //// Login with Google account
        //private string ClientId = ConfigurationManager.AppSettings["Google.ClientID"];
        //private string SecretKey = ConfigurationManager.AppSettings["Google.SecretKey"];
        //private string RedirectUrl = ConfigurationManager.AppSettings["Google.RedirectUrl"];

        public LoginController(UserManager<User> userManager,
                    SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var userName = model.Email;

                if (model.Email.IndexOf('@') > -1)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError("Email", "Email không khả dụng");
                        return View(model);
                    }
                    else
                    {
                        userName = user.UserName;
                    }
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError("Email", "Số điện thoại không khả dụng");
                        return View(model);
                    }
                    else
                    {
                        userName = user.UserName;
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(userName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(String.Empty, "Sai email hoặc password");

                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToAction(nameof(Lockout));
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Đăng nhập không hợp lệ");
                //    return View(model);
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PopupSignIn(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var userName = model.Email;

                if (model.Email.IndexOf('@') > -1)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(String.Empty, "Email không khả dụng");
                        return View(model);
                    }
                    else
                    {
                        userName = user.UserName;
                    }
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(String.Empty, "Số điện thoại không khả dụng");
                        return View(model);
                    }
                    else
                    {
                        userName = user.UserName;
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(userName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    return Json(new { message = "success" });
                    //return RedirectToAction("Register", "User");
                }
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToAction(nameof(Lockout));
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Đăng nhập không hợp lệ");
                //    return View(model);
                //}
            }

            // If we got this far, something failed, redisplay form
            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterViewModel();
            model.Sex = "Nam";

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Check mail already exist
                var alreadyMail = await _userManager.FindByEmailAsync(model.Email);
                if (alreadyMail != null)
                {
                    ModelState.AddModelError("Email", "Email đã được đăng ký");
                    return View(model);
                }

                // Check phonenumber already exist
                var alreadyPhone = await _userManager.FindByNameAsync(model.PhoneNumber);
                if (alreadyPhone != null)
                {
                    ModelState.AddModelError("PhoneNumber", "Số điện thoại đã được đăng ký");
                    return View(model);
                }

                var user = new User
                {
                    HoTen = model.HoTen,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.PhoneNumber,
                    BirthDate = DateTime.Parse(model.DateOfBirth + "/" + model.MonthOfBirth + "/" + model.YearOfBirth),
                    Sex = model.Sex
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToLocal(returnUrl);
                    //return RedirectToAction("Profile", "User");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}