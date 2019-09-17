using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Empyreal.ViewModels.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;

namespace Empyreal.Controllers.Manager
{
    public class OrderController : Controller
    {
        #region --- Variables ---
        // Service
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductPriceService priceService;
        private readonly IHistoryService historyService;
        private readonly IProductTypeService productTypeService;
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;

        private readonly IImageService imageService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        // Environment

        // Constant 

        private const string TABLE_NAME = "Order";
        #endregion --- Variables ---

        #region --- Constructor ---

        public OrderController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            historyService = ServiceLocator.Current.GetInstance<IHistoryService>();
            productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            orderService = ServiceLocator.Current.GetInstance<IOrderService>();
            orderDetailService = ServiceLocator.Current.GetInstance<IOrderDetailService>();
        }

        #endregion --- Constructor ---

        public async Task<IActionResult> OrderManager(string KeyWord, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(KeyWord))
            {
                KeyWord = string.Empty;
            }
            var ENVIRONMENT_USER_ID = await userManager.GetUserAsync(User);
            if (ENVIRONMENT_USER_ID == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            var role = userManager.GetRolesAsync(ENVIRONMENT_USER_ID).Result;
            var isShipper = false;
            if(role.Contains("Shipper"))
            {
                isShipper = true;
            }

            List<Order> model = new List<Order>();
            
            var userList = await userManager.Users
                .ToAsyncEnumerable()
                .Where(user => user.HoTen.ToLower().Contains(KeyWord.ToLower()))
                .Select(user => user.Id)
                .ToList();

            if (isShipper)
                
            {   // Đăng nhập là shipper
                model = orderService.GetListForShipper(ENVIRONMENT_USER_ID.Id, userList, KeyWord).ToList();
            }
            else
            {   // Đăng nhập là Admin
                model = orderService.GetList(userList, KeyWord).ToList();

            }
            OrderManagerViewModel viewModel = new OrderManagerViewModel();
            IEnumerable<OrderViewModel> orderDTO = MappingViewModel(model);
            viewModel.PagedOrderModel = new PagedList<OrderViewModel>(orderDTO.AsQueryable(), page,pageSize);
            viewModel.Keyword = KeyWord;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OrderUpdate(int orderID)
        {
            OrderUpdateViewModel viewModel = new OrderUpdateViewModel();
            viewModel.ProductDetail = new List<ProductDetailBasicViewModel>();
            viewModel.IsError = true;

            var ENVIRONMENT_USER_ID = await userManager.GetUserAsync(User);
            if (ENVIRONMENT_USER_ID == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            var role = userManager.GetRolesAsync(ENVIRONMENT_USER_ID).Result;
            var isShipper = false;
            if (role.Contains("Shipper"))
            {
                isShipper = true;
            }

            viewModel.UserID = ENVIRONMENT_USER_ID.Id;
            viewModel.UserName = ENVIRONMENT_USER_ID.UserName;

            Order orderModel = orderService.Get(orderID);
            if (orderModel == null)
                return View(viewModel);
            viewModel.SumPrice = orderModel.PriceSum; // Giá tổng
            viewModel.ShippingFee = orderModel.ShippingFee; // Phí vận chuyển
            viewModel.FinalPrice = orderModel.PriceSum + orderModel.ShippingFee; // Tổng tiền thu
            viewModel.CreateDate = orderModel.CreateDate;
            viewModel.OrderID = orderID;
            viewModel.State = orderModel.State;
            viewModel.Shipper = orderModel.Shipper;
            viewModel.ReceiverAddress = orderModel.Address;
            viewModel.ReceiverName = orderModel.Name;
            viewModel.ReceiverPhone = orderModel.PhoneNumber;
            if (isShipper)
            {
                viewModel.ShippingDate = (orderModel.ShippingDate == null) ? DateTime.Now : orderModel.ShippingDate;
            }
            viewModel.DeadlineShip = (orderModel.DeadlineShip == null) ? DateTime.Now : orderModel.DeadlineShip;
            IEnumerable<OrderDetail> orderDetailModel = orderDetailService.GetListByOrder(orderID);
            List<ProductDetailBasicViewModel> productDetail = new List<ProductDetailBasicViewModel>();
            foreach (var orderDetail in orderDetailModel)
            {
                ProductDetail detail = productDetailService.GetOne(orderDetail.ProductDetailId);
                detail.PriceNavigation = priceService.GetOne((Int32)detail.Price);
                detail.ColorNavigation = productTypeService.GetOne((Int32)detail.Color);
                detail.SizeNavigation = productTypeService.GetOne((Int32)detail.Size);

                ProductDetailBasicViewModel product = new ProductDetailBasicViewModel(detail);
                product.PriceText = orderDetail.Price;
                product.Quantity = orderDetail.Quantity;
                product.ProductName = detail.Product.Name;
                product.SumPriceText = product.PriceText * product.Quantity;
                productDetail.Add(product);
            }
            viewModel.ProductDetail = productDetail;
            viewModel.CbbShipper = GetShippers();
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> OrderUpdate(OrderUpdateViewModel viewModel)
        {
            var orderModel = orderService.Get(viewModel.OrderID);
            if(orderModel == null)
                return View(viewModel);

            var ENVIRONMENT_USER_ID = await userManager.GetUserAsync(User);
            if (ENVIRONMENT_USER_ID == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            orderModel.LastModifyByUser = ENVIRONMENT_USER_ID.Id;
            orderModel.LastModifyDate = DateTime.Now;
            orderModel.DeadlineShip = viewModel.DeadlineShip;
            orderModel.ShippingDate = viewModel.ShippingDate;

            orderModel.Shipper = viewModel.Shipper;
            orderModel.State++;

            int isReturn = 0;
            // Execute Insert
            try
            {              
                isReturn = orderService.UpdateOrder(orderModel);

                viewModel.IsError = false; // success
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;

            }

            return View(viewModel);
        }

        public List<SelectListItem> GetShippers()
        {
            var Shipper = userManager.GetUsersInRoleAsync("Shipper").Result;


            //
            List<SelectListItem> listShipper = new List<SelectListItem>();
            foreach(var user in Shipper)
            {
                listShipper.Add(new SelectListItem() { Text = user.HoTen, Value = user.Id });
            }

            return listShipper;
        }


        private List<OrderViewModel> MappingViewModel(List<Order> model)
        {
            List<OrderViewModel> viewModel = new List<OrderViewModel>();


            foreach (Order item in model)
            {
                OrderViewModel temp = new OrderViewModel(item);
                User user = userService.Get(item.UserId);
                if(user != null)
                {
                    temp.UserName =  user.HoTen;
                }
                User shipper = userService.Get(item.Shipper);
                if (shipper != null)
                {
                    temp.ShipperName = shipper.HoTen;
                }

                if (temp.FinalPrice != null)
                {
                    temp.PriceSumText = String.Format("{0:n0} VNĐ", temp.FinalPrice);
                }
                viewModel.Add(temp);
            }

            return viewModel;
        }
    }
}