using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Empyreal.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Empyreal.Controllers.Manager
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> UserManager(string keySearch, int page = 1, int pageSize = 5)
        {
            if (string.IsNullOrWhiteSpace(keySearch))
                keySearch = string.Empty;
            //var user = await userManager.GetUserAsync(User);
            List<User> users = new List<User>();
            users = userService.AllUser(keySearch);
            //
            List<UserBasicViewModel> listUser = new List<UserBasicViewModel>();
            foreach (var u in users)
            {
                // Get roles of user
                List<string> roles = new List<string>(await userManager.GetRolesAsync(u));

                UserBasicViewModel user = new UserBasicViewModel()
                {
                    ID = u.Id,
                    HoTen = u.HoTen,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Roles = roles,
                    State = u.State
                };
                listUser.Add(user);
            }
            UserManagerViewModel model = new UserManagerViewModel(listUser, page, pageSize, keySearch);
            return View(model);
        }

        public IActionResult AddUser(bool isSuccess)
        {
            var roles = roleManager.Roles.ToList();

            var model = new UserUpdateViewModel();
            model.IsSuccess = isSuccess;
            model.Sex = "Nam";
            model.UserRoles = roles.Select(s => new SelectListItem
            {
                Value = s.Name,
                Text = s.Name
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserUpdateViewModel model)
        {
            // Set roles of model to display in dropdownlist
            var roles = roleManager.Roles.ToList();
            model.UserRoles = roles.Select(s => new SelectListItem
            {
                Value = s.Name,
                Text = s.Name
            });

            if (ModelState.IsValid)
            {
                // Check mail already exist
                var alreadyMail = await userManager.FindByEmailAsync(model.Email);
                if (alreadyMail != null)
                {
                    ModelState.AddModelError("Email", "Email đã được đăng ký");
                    return View(model);
                }

                // Check phonenumber already exist
                var alreadyPhone = await userManager.FindByNameAsync(model.PhoneNumber);
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
                    Sex = model.Sex,
                    State = 1
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //logger.LogInformation("User created a new account with password.");
                    if (String.IsNullOrEmpty(model.UserRole))
                    {
                        model.UserRole = "User";
                    }
                    await userManager.AddToRoleAsync(user, model.UserRole);

                    model.IsSuccess = true;
                    ModelState.Clear();
                    //return View(model);
                    return RedirectToAction("AddUser", "User", new { isSuccess = true });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IActionResult UpdateUser(string Id)
        {
            var user = userService.Get(Id);
            if (user == null)
            {
                return RedirectToAction("UserManager", "User");
            }

            var roles = roleManager.Roles.ToList();
            var modelRoles = roles.Select(s => new SelectListItem
            {
                Value = s.Name,
                Text = s.Name
            });

            var model = new UserUpdateViewModel
            {
                HoTen = user.HoTen,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRoles = modelRoles,
                UserRole = userManager.GetRolesAsync(user).Result.First(),
                DateOfBirth = user.BirthDate.Date.ToString("dd"),
                MonthOfBirth = user.BirthDate.Date.ToString("MM"),
                YearOfBirth = user.BirthDate.Date.ToString("yyyy"),
                Sex = user.Sex,
                IsSuccess = false
            };

            //var userRole = userManager.GetRolesAsync(user).Result.First();
            //if (String.IsNullOrEmpty(userRole))
            //{
            //    model.UserRole = null;
            //}
            //else
            //{
            //    model.UserRole = userRole;
            //}

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            var roles = roleManager.Roles.ToList();
            model.UserRoles = roles.Select(s => new SelectListItem
            {
                Value = s.Name,
                Text = s.Name
            });

            var user = userService.Get(model.Id);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Người dùng không tồn tại");
                return View(model);
            }

            else
            {
                var role = userManager.GetRolesAsync(user).Result.First();
                if (role != model.UserRole)
                {
                    await userManager.RemoveFromRoleAsync(user, role);
                    await userManager.AddToRoleAsync(user, model.UserRole);
                }

                user.HoTen = model.HoTen;
                user.Sex = model.Sex;
                user.BirthDate = DateTime.Parse(model.DateOfBirth + "/" + model.MonthOfBirth + "/" + model.YearOfBirth);

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //logger.LogInformation("Update thành công.");
                    model.IsSuccess = true;
                    model.Email = user.Email;
                    model.PhoneNumber = user.PhoneNumber;
                    return View(model);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DisableUser(string Id)
        {
            var user = userService.Get(Id);
            if (user == null)
            {
                return RedirectToAction("UserManager", "User");
            }
            user.State = 0;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                //logger.LogInformation("Update thành công.");
                return RedirectToAction("UserManager", "User");
            }

            return RedirectToAction("UserManager", "User");
        }

        [HttpGet]
        public async Task<IActionResult> EnableUser(string Id)
        {
            var user = userService.Get(Id);
            if (user == null)
            {
                return RedirectToAction("UserManager", "User");
            }
            user.State = 1;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                //logger.LogInformation("Update thành công.");
                return RedirectToAction("UserManager", "User");
            }

            return RedirectToAction("UserManager", "User");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}