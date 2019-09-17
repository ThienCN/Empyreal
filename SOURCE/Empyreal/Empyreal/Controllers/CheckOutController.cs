using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Hubs;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Empyreal.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly IProvinceService provinceService;
        private readonly IDistrictService districtService;
        private readonly IWardService wardService;
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductPriceService priceService;
        private readonly IProductTypeService productTypeService;
        private readonly ICartDetailService cartDetailService;
        private readonly IEmailService emailService;
        private readonly UserManager<User> userManager;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CheckOutController(UserManager<User> userManager, IHubContext<ChatHub> hubContext, IHostingEnvironment hostingEnvironment)
        {
            provinceService = ServiceLocator.Current.GetInstance<IProvinceService>();
            districtService = ServiceLocator.Current.GetInstance<IDistrictService>();
            wardService = ServiceLocator.Current.GetInstance<IWardService>();
            orderService = ServiceLocator.Current.GetInstance<IOrderService>();
            orderDetailService = ServiceLocator.Current.GetInstance<IOrderDetailService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            cartDetailService = ServiceLocator.Current.GetInstance<ICartDetailService>();
            emailService = ServiceLocator.Current.GetInstance<IEmailService>();
            this.userManager = userManager;
            _hubContext = hubContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Shipping(CartViewModel cartViewModel)
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

            // Remove products which out of stock
            cartViewModel.Cart = cartViewModel.Cart.Where(cd => cd.BuyedQuantity != 0).ToList();

            // Mapping to view model
            ShippingViewModel model = new ShippingViewModel()
            {
                Id = user.Id,
                Name = user.HoTen,
                PhoneNumber = user.PhoneNumber,
                Provinces = provinceViewModels,
                Districts = districtViewModels,
                Wards = wardViewModels,
                CartViewModel = cartViewModel
            };

            // Get old order's info of current user
            order = orderService.GetOrderOfUser(user.Id);
            if (order != null)
            {
                orderViewModel = new OrderViewModel(order);
                model.Order = orderViewModel;
            }

            return View("Shipping", model);
        }

        [HttpPost]
        public IActionResult Pay(ShippingViewModel shippingModel)
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
                return Payment(shippingModel);
            }
            return View(shippingModel);
        }

        public IActionResult Payment(ShippingViewModel shippingModel)
        {
            if (shippingModel.Order != null && shippingModel.Order.Id > 0)
            {
                shippingModel.Id = shippingModel.Order.UserId;
                shippingModel.Address = shippingModel.Order.Address;
                shippingModel.AddressType = shippingModel.Order.AddressType;
                shippingModel.Name = shippingModel.Order.Name;
                shippingModel.PhoneNumber = shippingModel.Order.PhoneNumber;
            }

            //ShippingViewModel shippingModel = (ShippingViewModel)TempData["ShippingViewModel"];
            double sumPrice = 0;
            double shippingFee = 0;
            DateTime date = DateTime.Now.AddDays(3);
            string dayOfWeek = date.DayOfWeek.ToString();
            string dateShipping = date.ToString("dd/MM/yyyy");
            ChangeDay(ref dayOfWeek);

            string dateText = string.Format("{0}, {1}", dayOfWeek, dateShipping);

            foreach (var item in shippingModel.CartViewModel.Cart)
            {
                sumPrice += item.BuyedQuantity.GetValueOrDefault()
                    * item.ProductDetail.PriceText.GetValueOrDefault();
            }

            if (sumPrice < 500000)
            {
                shippingFee = sumPrice * 0.05;
            }

            PaymentViewModel paymentViewModel = new PaymentViewModel()
            {
                Shipping = shippingModel,
                DateText = dateText,
                Products = shippingModel.CartViewModel,
                TempPrice = sumPrice,
                ShippingFee = shippingFee,
                IsError = false,
                Message = string.Empty
            };

            return View("Payment", paymentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel paymentViewModel)
        {
            if (ModelState.IsValid)
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                int isReturn = 0; // Success or fail
                string message = string.Empty; // Message
                ProductDetail productDetailUpdate; // Update product detail
                List<ProductDetail> lstProductDetailUpdate = new List<ProductDetail>(); // Update product detail list
                CartDetail cartDetailUpdate; // Update cart detail
                List<CartDetail> lstCartDetailUpdate = new List<CartDetail>(); // Update cart detail list

                foreach (var item in paymentViewModel.Products.Cart)
                {
                    productDetailUpdate = productDetailService.GetOne(item.ProductDetail.ID);
                    if (productDetailUpdate != null)
                    {
                        if (productDetailUpdate.Quantity <= 0)
                        {
                            isReturn = 0;
                            message = "outofstock";
                            goto Finish;
                        }
                        lstProductDetailUpdate.Add(productDetailUpdate);
                    }
                }

                Order order = null; // Order Entity
                try
                {
                    DataTranfer(out order, paymentViewModel);
                }
                catch (Exception e)
                {
                    message = e.Message;
                    goto Finish;
                }

                // Call Service => Execute
                #region --- Main Execute ---

                // Create new order
                result = orderService.Create(order);

                // Check create new order success or fail
                paymentViewModel.OrderId = result.Keys.FirstOrDefault();
                if (paymentViewModel.OrderId <= 0) // Create fail
                {
                    result.TryGetValue(isReturn, out message); // Get error message
                    goto Finish;
                }
                else // Add new order successful
                {
                    try
                    {
                        DataTranferOrderDetail(ref order, paymentViewModel);
                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                        goto Finish;
                    }
                }

                result.Clear();
                // Create new order detail
                result = orderDetailService.AddOrderDetail(order);

                // Check create new order detail success or fail
                isReturn = result.Keys.FirstOrDefault();
                if (isReturn <= 0) // Create fail
                {
                    result.TryGetValue(isReturn, out message); // Get error message
                    goto Finish;
                }
                else // Create order detail successful, then update quantity of product
                {
                    // Update quantity of product
                    for (int i = 0; i < lstProductDetailUpdate.Count; i++)
                    {
                        lstProductDetailUpdate[i].Quantity -= paymentViewModel.Products.Cart[i].BuyedQuantity;
                    }
                    isReturn = productDetailService.Update(lstProductDetailUpdate); // Update

                    if (isReturn <= 0)
                    {
                        message = "Cập nhật số lượng sản phẩm không thành công";
                        goto Finish;
                    }

                    // Update cart detail
                    foreach (var item in paymentViewModel.Products.Cart)
                    {
                        cartDetailUpdate = cartDetailService.Get(item.CartDetailId);
                        if (cartDetailUpdate != null)
                        {
                            cartDetailUpdate.State = 2;
                            lstCartDetailUpdate.Add(cartDetailUpdate);
                        }
                    }
                    isReturn = cartDetailService.Updates(lstCartDetailUpdate); // Update

                    if (isReturn <= 0)
                    {
                        message = "Cập nhật trạng thái của sản phẩm trong giỏ hàng không thành công";
                        goto Finish;
                    }
                }

                Finish:
                // isReturn = 0: Error || 1: Success
                // Giải thích: isReturn = dbContext.Commit = số dòng được thay đổi trong sql
                paymentViewModel.IsError = (isReturn == 0);
                paymentViewModel.Message = message;

                #endregion --- Main Execute ---

                if (paymentViewModel.IsError) // Error
                    return View(paymentViewModel);

                List<ProductDetail> temp = new List<ProductDetail>();
                foreach (var item in lstProductDetailUpdate)
                {
                    productDetailUpdate = new ProductDetail()
                    {
                        Id = item.Id,
                        Quantity = item.Quantity
                    };
                    temp.Add(productDetailUpdate);
                }

                // Call SignalR update quantity
                await _hubContext.Clients.All.SendAsync("ReloadQuantity", temp);

                // Call SignalR update statistical
                await _hubContext.Clients.All.SendAsync("ReloadStatistical");

                // Send email
                User user = await userManager.GetUserAsync(User);
                SendEmail(paymentViewModel, user.Email);

                // Call SignalR notification
                await _hubContext.Clients.All.SendAsync("OrderSuccess");

                paymentViewModel.Email = user.Email;
                paymentViewModel.IsPaymentSuccess = true;
                return View(paymentViewModel);
            }

            //    //string userIDProc = order.UserId;
            //    //DataTable dataTable = new DataTable();
            //    //dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            //    //dataTable.Columns.Add(new DataColumn("Quantity", typeof(int)));
            //    //dataTable.Columns.Add(new DataColumn("Processed", typeof(int)));
            //    //dataTable.Columns.Add(new DataColumn("UserIDProc", typeof(string)));
            //    //foreach(var item in order.OrderDetail)
            //    //{
            //    //    dataTable.Rows.Add(item.ProductDetailId, item.Quantity, 0, userIDProc);
            //    //}
            //    //string query = "UpdateProductDetail @ListID";
            //    //orderDetailService.ExecStoreUpdate(query, new SqlParameter("@ListID", dataTable));

            return View(paymentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeliveryAddress(CartViewModel cartViewModel)
        {
            return await Shipping(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDeliveryAddress(PaymentViewModel paymentViewModel)
        {
            return await Shipping(paymentViewModel.Products);
        }

        [HttpPost]
        public IActionResult Order(ShippingViewModel shippingViewModel)
        {
            return Payment(shippingViewModel);
        }

        public void DataTranfer(out Order order, PaymentViewModel paymentViewModel)
        {
            // ViewModel => Entity
            #region --- Tranfer ---

            if (!paymentViewModel.IsRelative)
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
                    PaymentType = paymentViewModel.PaymentType,
                    ShippingType = paymentViewModel.ShippingType
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
                    Name = paymentViewModel.RelativeName,
                    PhoneNumber = paymentViewModel.RelativePhoneNumber,
                    ShippingDate = DateTime.Now.AddDays(3),
                    ShippingFee = paymentViewModel.ShippingFee,
                    PaymentType = paymentViewModel.PaymentType,
                    ShippingType = paymentViewModel.ShippingType
                };
            }

            #endregion --- Tranfer ---

        }

        public void DataTranferOrderDetail(ref Order order, PaymentViewModel paymentViewModel)
        {
            // ViewModel => Entity
            var orderDetails = new List<OrderDetail>();
            OrderDetail orderDetail;
            foreach (var item in paymentViewModel.Products.Cart)
            {
                orderDetail = new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductDetailId = item.ProductDetail.ID,
                    Price = item.ProductDetail.PriceText,
                    Quantity = item.BuyedQuantity.GetValueOrDefault(),
                    State = 1
                };
                orderDetails.Add(orderDetail);
            }
            order.OrderDetail = orderDetails;
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

        public IActionResult TestSendEmail()
        {
            return View();
        }

        public void SendEmail(PaymentViewModel model, string email)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.Content = CreateEmailBody(model, email);
            emailMessage.ToAddress = email;
            emailMessage.Subject = string.Format("Xác nhận đơn hàng #{0}", model.OrderId);

            emailService.Send(emailMessage);
        }

        [HttpGet]
        public IActionResult TestTemplate()
        {
            return View();
        }

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

        private string CreateEmailBody(PaymentViewModel model, string email)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

            // Reading html template
            string body = string.Empty;
            string rootFolder = _hostingEnvironment.WebRootPath;
            body = System.IO.File.ReadAllText(rootFolder + "/SendMailTemplate/SendMailTemplate.cshtml");

            // Replacing the required things
            body = body.Replace("{OrderID}", model.OrderId.ToString());
            body = body.Replace("{OrderDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            body = body.Replace("{Email}", email);
            body = body.Replace("{ShippingAddress}", model.Shipping.Address);
            if (!model.IsRelative)
            {
                body = body.Replace("{UserName}", model.Shipping.Name);
                body = body.Replace("{PhoneNumber}", model.Shipping.PhoneNumber);
            }
            else
            {
                body = body.Replace("{UserName}", model.RelativeName);
                body = body.Replace("{PhoneNumber}", model.RelativePhoneNumber);
            }

            body = body.Replace("{ShippingDate}", model.DateText);
            body = body.Replace("{ShippingFee}", model.DisplayShippingFee);

            string products = "";
            foreach (var item in model.Products.Cart)
            {
                double itemTempSum = (item.BuyedQuantity * item.ProductDetail.PriceText).GetValueOrDefault();
                string displayPrice = string.Format(cul, "{0:c0}", itemTempSum);

                products += string.Format(@"<tr style=""background: #eee;"">
                            <td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                        </tr>", item.Product.Name, item.ProductDetail.DisplayPrice, item.BuyedQuantity, displayPrice);
            }

            body = body.Replace("{Products}", products);
            body = body.Replace("{TempSum}", model.DisplayTempPrice);
            body = body.Replace("{Sum}", model.DisplayFinalPrice);

            return body;
        }
    }
}