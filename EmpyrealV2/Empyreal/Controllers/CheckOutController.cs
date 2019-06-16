using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace Empyreal.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly IProvinceService provinceService;
        private readonly IDistrictService districtService;
        private readonly IWardService wardService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductPriceService priceService;
        private readonly IProductTypeService productTypeService;
        private readonly UserManager<User> userManager;
        private static List<CartDetailViewModel> cartDetailViewModels = new List<CartDetailViewModel>(); // List cart detail view model
        private static PaymentViewModel paymentViewModel;

        public CheckOutController(UserManager<User> userManager)
        {
            provinceService = ServiceLocator.Current.GetInstance<IProvinceService>();
            districtService = ServiceLocator.Current.GetInstance<IDistrictService>();
            wardService = ServiceLocator.Current.GetInstance<IWardService>();
            orderService = ServiceLocator.Current.GetInstance<IOrderService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            this.userManager = userManager;
        }

        public async Task<IActionResult> Shipping()
        {
            List<Province> provinces; // List of Province
            List<District> districts; // List of District
            List<Ward> wards; // List of Ward
            ProvinceViewModel provinceViewModel; // Province view model
            List<ProvinceViewModel> provinceViewModels = new List<ProvinceViewModel>(); // List of Province view model
            DistrictViewModel districtViewModel; // District view model
            List<DistrictViewModel> districtViewModels = new List<DistrictViewModel>(); // List of District view model
            WardViewModel wardViewModel; // Ward view model
            List<WardViewModel> wardViewModels = new List<WardViewModel>(); // List of Ward view model
            Order order; // List of Order
            OrderViewModel orderViewModel; // Order view model

            // Get current User
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            // Get all Provinces and mapping to view model
            provinces = provinceService.GetAll();
            foreach (var item in provinces)
            {
                provinceViewModel = new ProvinceViewModel(item);
                provinceViewModels.Add(provinceViewModel);
            }

            var hcmProvince = provinces.Where(p => p.Id == 79).First();
            // Get first District and mapping to view model
            districts = districtService.Get(hcmProvince.Id);
            foreach (var item in districts)
            {
                districtViewModel = new DistrictViewModel(item);
                districtViewModels.Add(districtViewModel);
            }

            // Get first Ward and mapping to view model
            wards = wardService.Get(districts.First().Id);
            foreach (var item in wards)
            {
                wardViewModel = new WardViewModel(item);
                wardViewModels.Add(wardViewModel);
            }

            // Mapping to view model
            ShippingViewModel model = new ShippingViewModel()
            {
                Id = user.Id,
                Name = user.HoTen,
                PhoneNumber = user.PhoneNumber,
                Provinces = provinceViewModels,
                Districts = districtViewModels,
                Wards = wardViewModels
            };

            // Get old order's info of current user
            order = orderService.GetOrderOfUser(user.Id);
            if (order != null)
            {
                orderViewModel = new OrderViewModel(order);
                model.Order = orderViewModel;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Shipping(ShippingViewModel shippingModel)
        {
            if (ModelState.IsValid)
            {
                //TempData["ShippingViewModel"] = model;

                var w = wardService.GetById(int.Parse(shippingModel.Ward));
                var d = districtService.GetById(int.Parse(shippingModel.District));
                var p = provinceService.GetById(int.Parse(shippingModel.Province));
                string ward = string.Format("{0} {1}", w.Type, w.Name);
                string district = string.Format("{0} {1}", d.Type, d.Name);
                string province = string.Format("{0} {1}", p.Type, p.Name);

                shippingModel.Address = string.Format("{0}, {1}, {2}, {3}",
                    shippingModel.Address, ward, district, province);
                return RedirectToAction("Payment", "CheckOut", new RouteValueDictionary(shippingModel));
            }
            return View(shippingModel);
        }

        [HttpPost]
        public IActionResult ProvinceChange(int provinceID)
        {
            var districts = districtService.Get(provinceID);
            var wards = wardService.Get(districts.First().Id);

            if (districts == null || districts.Count <= 0)
            {
                return Json(new { isSuccess = false, message = "Không có district" });
            }
            if (wards == null || wards.Count <= 0)
            {
                return Json(new { isSuccess = false, message = "Không có wards" });
            }
            return Json(new { isSuccess = true, districts = districts, wards = wards });
        }

        [HttpPost]
        public IActionResult DistrictChange(int districtID)
        {
            var wards = wardService.Get(districtID);

            if (wards == null || wards.Count <= 0)
            {
                return Json(new { isSuccess = false, message = "Không có wards" });
            }
            return Json(new { isSuccess = true, wards = wards });
        }

        public IActionResult Payment(ShippingViewModel shippingModel, int addressID)
        {
            if (addressID > 0)
            {
                var oldOrder = orderService.Get(addressID);
                shippingModel.Id = oldOrder.UserId;
                shippingModel.Address = oldOrder.Address;
                shippingModel.AddressType = oldOrder.AddressType;
                shippingModel.Name = oldOrder.Name;
                shippingModel.PhoneNumber = oldOrder.PhoneNumber;                
            }

            //ShippingViewModel shippingModel = (ShippingViewModel)TempData["ShippingViewModel"];
            double sumPrice = 0;
            double shippingFee = 0;
            DateTime date = DateTime.Now.AddDays(3);
            string dayOfWeek = date.DayOfWeek.ToString();
            string dateShipping = date.ToString("dd/MM/yyyy");
            ChangeDay(ref dayOfWeek);

            string dateText = string.Format("{0}, {1}", dayOfWeek, dateShipping);

            for (int i = 0; i < cartDetailViewModels.Count; i++)
            {
                sumPrice += cartDetailViewModels[i].ProductDetail.Quantity.GetValueOrDefault()
                    * cartDetailViewModels[i].ProductDetail.PriceText.GetValueOrDefault();
            }

            if (sumPrice < 500000)
            {
                shippingFee = sumPrice * 0.05;
            }

            paymentViewModel = new PaymentViewModel()
            {
                Shipping = shippingModel,
                DateText = dateText,
                Products = cartDetailViewModels,
                TempPrice = sumPrice,
                ShippingFee = shippingFee
            };

            return View(paymentViewModel);
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int isReturn = 0;
                string message = string.Empty;

                // Oder Entity
                Order order = null;
                //Excute
                try
                {
                    DataTranfer(out order, viewModel);
                }
                catch (Exception e)
                {
                    message = e.Message;
                    goto Finish;
                }

                // Call Service => Execute
                #region --- Main Execute ---

                // Execute Insert
                try
                {
                    // Create
                    isReturn = orderService.Create(order);
                }
                catch (Exception e)
                {
                    message = e.Message;
                    goto Finish;
                }

                if (isReturn > 0)
                {

                }

                Finish:
                    // isReturn = 0: Error || 1: Success
                    // Giải thích: isReturn = dbContext.Commit = số dòng được thay đổi trong sql
                    viewModel.IsError = (isReturn == 0);
                    viewModel.Message = message;

                #endregion --- Main Execute ---

                return View(paymentViewModel);
            }

            return View(paymentViewModel);
        }

        [HttpPost]
        public IActionResult BuyedProduct(List<int> lst)
        {
            ProductDetail productDetail;
            CartDetailViewModel cartDetailViewModel; // Cart detail view model
            Product product; // Get product in cart
            int productDetailID; // ID of product detail
            int priceID; // Get price ID of product detail
            int sizeID; // Get size ID of product detail
            int colorID; // Get color ID of product detail
            int productID; // ID of product
            List<ProductType> productTypes; // Get size and color of product detail

            cartDetailViewModels = new List<CartDetailViewModel>();

            for (int i = 0; i < lst.Count; i = i + 2)
            {
                // Get detail of product
                productDetailID = lst[i];
                productDetail = productDetailService.GetOne(productDetailID);

                // Get price of product detail
                priceID = productDetail.Price.GetValueOrDefault();
                productDetail.PriceNavigation = priceService.GetOne(priceID);

                // Get size and color of product detai
                sizeID = productDetail.Size.GetValueOrDefault();
                colorID = productDetail.Color.GetValueOrDefault();
                productTypes = productTypeService.GetSizeColor(sizeID, colorID);
                productDetail.SizeNavigation = productTypes[0];
                productDetail.ColorNavigation = productTypes[1];
                productDetail.Quantity = lst[i + 1];

                productID = productDetail.ProductId.GetValueOrDefault();
                product = productService.Get(productID);

                cartDetailViewModel = new CartDetailViewModel(product, productDetail, string.Empty, 0);
                cartDetailViewModels.Add(cartDetailViewModel);
            }

            if (cartDetailViewModels.Count > 0)
                return Json(new
                {
                    isSuccess = true,
                    url = "/CheckOut/Shipping"
                });

            return Json(new
            {
                isSuccess = false,
                message = "No item chosen"
            });
        }

        public void DataTranfer(out Order order, PaymentViewModel viewModel)
        {
            // ViewModel => Entity
            #region --- Tranfer ---

            if (!viewModel.IsRelative) // 
            {
                // Order Entity => Order
                order = new Order()
                {
                    UserId = paymentViewModel.Shipping.Id,
                    PriceSum = paymentViewModel.TempPrice,
                    CreateDate = DateTime.Now,
                    State = 1,
                    Address = paymentViewModel.Shipping.Address,
                    AddressType = paymentViewModel.Shipping.AddressType,
                    Name = paymentViewModel.Shipping.Name,
                    PhoneNumber = paymentViewModel.Shipping.PhoneNumber,
                    ShippingDate = DateTime.Now.AddDays(3),
                    ShippingFee = paymentViewModel.ShippingFee,
                    PaymentType = viewModel.PaymentType,
                    ShippingType = viewModel.ShippingType
                };
            }
            else // Delivery to another
            {
                // Order Entity => Order
                order = new Order()
                {
                    UserId = paymentViewModel.Shipping.Id,
                    PriceSum = paymentViewModel.TempPrice,
                    CreateDate = DateTime.Now,
                    State = 1,
                    Address = paymentViewModel.Shipping.Address,
                    AddressType = paymentViewModel.Shipping.AddressType,
                    Name = viewModel.RelativeName,
                    PhoneNumber = viewModel.RelativePhoneNumber,
                    ShippingDate = DateTime.Now.AddDays(3),
                    ShippingFee = paymentViewModel.ShippingFee,
                    PaymentType = viewModel.PaymentType,
                    ShippingType = viewModel.ShippingType
                };
            }           
            
            #endregion --- Tranfer ---

        }

        //public void DataTranfer(out Order order)
        //{
        //    var orderDetails = new List<OrderDetail>();
        //    foreach(var item in paymentViewModel.Products)
        //    {

        //    }
        //    order.OrderDetail = orderDetails;
        //}

        public void ChangeDay(ref string dayOfWeek)
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
    }
}