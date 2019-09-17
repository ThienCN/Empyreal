using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Empyreal.ServiceLocators;

namespace Empyreal.Controllers.Manager
{
    [Authorize(Roles = "User")]
    public class SalesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductPriceService productPriceService;
        private readonly IProductTypeService productTypeService;
        private readonly IImageService imageService;

        public SalesController(UserManager<User> userManager)
        {
            _userManager = userManager;
            orderService = ServiceLocator.Current.GetInstance<IOrderService>();
            orderDetailService = ServiceLocator.Current.GetInstance<IOrderDetailService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            productPriceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
        }

        public async Task<IActionResult> Profile()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login"); // Redirect to Login page if user is null
            }
            else
            {
                // Convert to view model
                var profile = new ProfileViewModel
                {
                    HoTen = user.HoTen,
                    PhoneNumber = user.UserName,
                    Email = user.Email,
                    DateOfBirth = user.BirthDate.Date.ToString("dd"),
                    MonthOfBirth = user.BirthDate.Date.ToString("MM"),
                    YearOfBirth = user.BirthDate.Date.ToString("yyyy"),
                    Sex = user.Sex,
                    IsSuccess = false
                };
                return View(profile);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get current user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Người dùng không tồn tại");
                    return View(model);
                }
                else
                {
                    user.HoTen = model.HoTen;
                    user.Sex = model.Sex;
                    user.BirthDate = DateTime.Parse(model.YearOfBirth + "-" + model.MonthOfBirth + "-" + model.DateOfBirth);

                    var result = await _userManager.UpdateAsync(user); // Update profile of user
                    if (result.Succeeded) // Update profile successful
                    {
                        model.IsSuccess = true;

                        if (model.IsChangePass) // Is change password
                        {
                            result = _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword).Result; // Change password
                            if (!result.Succeeded)
                            {
                                model.IsSuccess = false;
                                ModelState.AddModelError(string.Empty, "Mật khẩu hiện tại không đúng");
                            }
                        }

                        model.Email = user.Email;
                        model.PhoneNumber = user.PhoneNumber;
                        return View(model);
                    }
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> Order()
        {
            OrderViewModel orderViewModel; // Order view model
            OrderDetailViewModel orderDetailViewModel; // Order detail view model
            List<OrderDetailViewModel> orderDetailViewModels = new List<OrderDetailViewModel>(); // Order detail view model list
            List<Order> orders; // Order model list
            OrderStateViewModel orderStateViewModel = new OrderStateViewModel(); // View model
            List<OrderDetail> orderDetails = new List<OrderDetail>(); // Order detail model list
            ProductDetail productDetail; // Product detail model
            ProductDetailBasicViewModel productDetailBasicViewModel; // Product detail view model
            List<ProductDetailBasicViewModel> productDetailBasicViewModels = new List<ProductDetailBasicViewModel>(); // Product detail view model list

            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            // Get orders of user
            orders = orderService.GetOrders(user.Id);
            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    // Set order
                    orderViewModel = new OrderViewModel(order);
                    productDetailBasicViewModels = new List<ProductDetailBasicViewModel>();

                    // Set product
                    orderDetails = orderDetailService.GetOrderDetails(order.Id);
                    if (orderDetails.Count > 0)
                    {
                        foreach (var orderDetail in orderDetails)
                        {
                            productDetail = productDetailService.GetOne(orderDetail.ProductDetailId);
                            productDetail.PriceNavigation = new ProductPrice();
                            productDetail.ColorNavigation = new ProductType();
                            productDetail.SizeNavigation = new ProductType();

                            productDetailBasicViewModel = new ProductDetailBasicViewModel(productDetail);
                            productDetailBasicViewModels.Add(productDetailBasicViewModel);
                        }
                    }
                    orderDetailViewModel = new OrderDetailViewModel(orderViewModel, productDetailBasicViewModels, null);
                    orderDetailViewModels.Add(orderDetailViewModel);
                }
                orderStateViewModel.Orders = orderDetailViewModels; // Tất cả đơn hàng
                orderStateViewModel.AcceptOrders = orderDetailViewModels.Where(o => o.Order.State == 1).ToList(); // Đơn hàng đang chờ xác nhận
                orderStateViewModel.ShippingOrders = orderDetailViewModels.Where(o => o.Order.State == 2).ToList(); // Đơn hàng đang giao
                orderStateViewModel.DoneOrders = orderDetailViewModels.Where(o => o.Order.State == 3).ToList(); // Đơn hàng giao thành công
                orderStateViewModel.CancelOrders = orderDetailViewModels.Where(o => o.Order.State == 0).ToList(); // Đơn hàng đã hủy
            }

            return View(orderStateViewModel);
        }

        public IActionResult OrderDetail(int orderId)
        {
            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel(); // Order detail view model
            OrderViewModel orderViewModel; // Order view model
            Order order; // Order model
            List<OrderDetail> orderDetails = new List<OrderDetail>(); // Order detail model list
            ProductDetail productDetail; // Product detail model
            ProductDetailBasicViewModel productDetailBasicViewModel; // Product detail view model
            List<ProductDetailBasicViewModel> productDetailBasicViewModels = new List<ProductDetailBasicViewModel>(); // Product detail view model list
            Image image; // Image model list
            ImageBasicViewModel imageBasicViewModel; // Image view model
            List<ImageBasicViewModel> imageBasicViewModels = new List<ImageBasicViewModel>(); // Image view model list

            // Get order
            order = orderService.Get(orderId);
            if (order == null)
            {
                return RedirectToAction("Order");
            }
            orderViewModel = new OrderViewModel(order);

            // Get order detail
            orderDetails = orderDetailService.GetOrderDetails(orderId);
            if (orderDetails.Count > 0)
            {
                foreach (var orderDetail in orderDetails) // Get product detail
                {
                    // Get product detail
                    productDetail = productDetailService.GetOne(orderDetail.ProductDetailId);
                    productDetail.PriceNavigation = productPriceService.GetOne(productDetail.Price.GetValueOrDefault()); // Get price
                    productDetail.ColorNavigation = productTypeService.GetOne(productDetail.Color.GetValueOrDefault()); // Get color
                    productDetail.SizeNavigation = productTypeService.GetOne(productDetail.Size.GetValueOrDefault()); // Get size                    

                    productDetailBasicViewModel = new ProductDetailBasicViewModel(productDetail);
                    productDetailBasicViewModel.Quantity = orderDetail.Quantity; // Set buyed quantity
                    productDetailBasicViewModels.Add(productDetailBasicViewModel);

                    // Get images
                    image = imageService.Get(productDetail.ProductId.GetValueOrDefault());
                    imageBasicViewModel = new ImageBasicViewModel(image);
                    imageBasicViewModels.Add(imageBasicViewModel);
                }
                orderDetailViewModel.Order = orderViewModel;
                orderDetailViewModel.ProductDetails = productDetailBasicViewModels;
                orderDetailViewModel.Images = imageBasicViewModels;
            }

            return View(orderDetailViewModel);
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