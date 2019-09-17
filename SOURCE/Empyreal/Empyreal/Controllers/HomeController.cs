using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.Interfaces.Services;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace Empyreal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IImageService imageService;
        private readonly IProductPriceService priceService;
        private readonly IProductTypeService productTypeService;
        private readonly ICartService cartService;
        private readonly ICartDetailService cartDetailService;
        private readonly IRateService rateService;
        private readonly IUserService userService;
        private readonly ICommonService commonService;
        private readonly UserManager<User> userManager;

        private const string TOP_PRODUCT = "Top";
        private const string NEW_PRODUCT = "New";
        private const string RAND_PRODUCT = "Random";

        public HomeController(UserManager<User> userManager)
        {
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            cartService = ServiceLocator.Current.GetInstance<ICartService>();
            cartDetailService = ServiceLocator.Current.GetInstance<ICartDetailService>();
            rateService = ServiceLocator.Current.GetInstance<IRateService>();
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            commonService = ServiceLocator.Current.GetInstance<ICommonService>();
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }     

        [AllowAnonymous]
        public IActionResult Product(int productID)
        {
            ProductBasicViewModel model; // View model
            ProductDetailBasicViewModel productDetailBasicViewModel; // Mapping data form model to view model
            ImageBasicViewModel imageBasicViewModel; // Mapping data form model to view model
            RateViewModel rateViewModel; // Mapping data form model to view model
            int priceID; // Get price ID of product detail
            int sizeID; // Get size ID of product detail
            int colorID; // Get color ID of product detail
            List<ProductType> productTypes; // Get size and color of product detail
            int totalRate = 0; // Get total of rates
            string[] starRate = new string[5];
            string query = "RatePercent @productID";

            // Get data of product
            Product product = new Product();
            product = (productID == 0) ? new Product() : productService.Get(productID);
            if (product == null)
            {
                return View();
            }

            var productDetails = productDetailService.GetList(productID); // Get product detail list
            var images = imageService.GetList(productID); // Get image list
            //var rates = rateService.GetRates(query,
            //                        new SqlParameter("@ProductID", productID)); // Get rate list
            var rates = rateService.GetRatesOfProduct(productID); // Get rate list

            // Mapping product
            model = new ProductBasicViewModel(product);

            // Mapping product detail
            foreach (var detail in productDetails)
            {
                // Get price of product detail
                priceID = detail.Price.GetValueOrDefault();
                detail.PriceNavigation = priceService.GetOne(priceID);

                // Get size and color of product detai
                sizeID = detail.Size.GetValueOrDefault();
                colorID = detail.Color.GetValueOrDefault();
                productTypes = productTypeService.GetSizeColor(sizeID, colorID);
                detail.SizeNavigation = productTypes[0];
                detail.ColorNavigation = productTypes[1];

                productDetailBasicViewModel = new ProductDetailBasicViewModel(detail);
                model.Details.Add(productDetailBasicViewModel);
            }

            model.GroupBySize = model.Details
                                .OrderBy(s => s.Size)
                                .GroupBy(s => s.Size)
                                .Select(grp => grp.ToList())
                                .ToList();

            // Mapping image
            foreach (var image in images)
            {
                imageBasicViewModel = new ImageBasicViewModel(image);
                model.Images.Add(imageBasicViewModel);
            }

            // Mapping rate
            foreach (var rate in rates)
            {
                rate.RateUser = userService.Get(rate.UserId);
                totalRate += rate.Star.GetValueOrDefault();
                rateViewModel = new RateViewModel(rate);
                model.Rates.Add(rateViewModel);
            }

            var ratePercents = rateService.GetRatePercent(query,
                                    new SqlParameter("@productID", productID));
            foreach (var ratePercent in ratePercents)
            {
                starRate[ratePercent.Star.GetValueOrDefault() - 1] = ratePercent.Contents;
            }
            for (int i = 0; i < 5; i++)
            {
                if (string.IsNullOrEmpty(starRate[i]))
                {
                    starRate[i] = "0%";
                }
                else
                {
                    starRate[i] += "%";
                }
            }

            model.PriceProduct = model.Details.First().PriceText; // Get price of first detail
            int conntRates = (rates.Count == 0) ? 1 : rates.Count;
            model.AvgRate = Math.Round((double)totalRate / conntRates, 2);
            model.RateList = starRate;
            return View(model);
        }

        public async Task<IActionResult> Cart()
        {
            User user; // Current User
            Cart cart; // Cart of User
            List<CartDetail> cartDetails; // Cart detail list of cart
            int productDetailID; // ID of product detail
            int productID; // ID of product
            //ProductDetailBasicViewModel productDetailBasicViewModel; // View model detail of product
            ProductDetail productDetail; // Model detail of product
            //ImageBasicViewModel imageViewModel; // View model image of product
            string imageURL; // Model image of product
            Product product; // Get product in cart
            int priceID; // Get price ID of product detail
            int sizeID; // Get size ID of product detail
            int colorID; // Get color ID of product detail
            List<ProductType> productTypes; // Get size and color of product detail
            CartDetailViewModel cartDetailViewModel; // Cart detail view model
            List<CartDetailViewModel> cartDetailViewModels = new List<CartDetailViewModel>(); // List cart detail view model
            CartViewModel model = new CartViewModel(); // View model
            int result = 0; // Check add cart is success

            // Get current user
            user = await userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return RedirectToAction("SignIn", "Login", new { returnUrl = Url.Action("Cart", "Home") });
            //}

            // Get cart of current user
            cart = cartService.Get(user.Id);
            if (cart == null)
            {
                // Add new cart
                cart = new Cart { UserId = user.Id, State = 1 };
                result = cartService.Create(cart);
                if (result <= 0)
                {
                    return Json(new
                    {
                        isSuccess = false,
                        message = "cart-error"
                    });
                }
            }

            // Get product of cart
            cartDetails = cartDetailService.GetAll(cart.Id);

            foreach (var item in cartDetails) // Mapping form model to view model
            {
                // Get detail of product
                productDetailID = item.ProductDetailId.GetValueOrDefault();
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

                // Get image of product
                productID = productDetail.ProductId.GetValueOrDefault();
                imageURL = imageService.Get(productID).Url.ToString();
                product = productService.Get(productID);

                cartDetailViewModel = new CartDetailViewModel(product, productDetail,
                    imageURL, item.Id, 0);

                cartDetailViewModels.Add(cartDetailViewModel);
            }
            model.Cart = cartDetailViewModels;

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Cart(CartViewModel cartViewModel)
        //{   

        //    if (cartViewModel.Cart.Count > 0)
        //        return RedirectToAction("Shipping", "CheckOut", new RouteValueDictionary(cartViewModel));
            
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddToCart(int productDetailId, int quantity)
        {
            User user = await userManager.GetUserAsync(User); // Get current user
            string userId; // Get Id of current user
            Cart existCart; // User has a cart
            Cart newCart; // User has not a cart
            int cartID; // Id of new cart
            CartDetail newCartDetail; // list cart detail
            int result = 0; // Check add cart is success

            if (user == null)
            {
                return Json(new
                {
                    isSuccess = false,
                    message = "null",
                    url = Url.Action("SignIn", "Login")
                });
            }
            userId = user.Id;

            existCart = cartService.Get(userId);
            if (existCart == null) // If user has not a cart, create a new cart
            {
                // Add new cart
                newCart = new Cart { UserId = userId, State = 1 };
                result = cartService.Create(newCart);
                if (result <= 0)
                {
                    return Json(new
                    {
                        isSuccess = false,
                        message = "cart-error"
                    });
                }

                // Add new cart detail to cart
                cartID = newCart.Id;
                newCartDetail = new CartDetail
                {
                    CartId = cartID,
                    ProductDetailId = productDetailId,
                    State = 1
                };
                result = cartDetailService.Create(newCartDetail);
                if (result <= 0)
                {
                    return Json(new
                    {
                        isSuccess = false,
                        message = "cart-detail-error"
                    });
                }
            }
            else // User has a cart, add new product to cart
            {
                newCartDetail = new CartDetail
                {
                    CartId = existCart.Id,
                    ProductDetailId = productDetailId,
                    State = 1
                };
                result = cartDetailService.Create(newCartDetail);
                if (result <= 0)
                {
                    return Json(new
                    {
                        isSuccess = false,
                        message = "cart-detail-error"
                    });
                }
            }

            return Json(new { isSuccess = true, message = "success" });
        }

        [HttpPost]
        public IActionResult RemoveItemInCart(int cartDetailId)
        {
            int result = 0;
            var cartDetail = cartDetailService.Get(cartDetailId);
            if (cartDetail != null)
            {
                cartDetail.State = 0;
                result = cartDetailService.Update(cartDetail);
                if (result <= 0)
                {
                    return Json(new { isSuccess = false, message = "Xóa sản phẩm không thành công" });
                }
                return Json(new { isSuccess = true, message = "Xóa sản phẩm khỏi giỏ hàng thành công" });
            }

            return Json(new { isSuccess = false, message = "Sản phẩm không tồn tại" });
        }

        public IActionResult GetPartial()
        {
            return PartialView("_UserInfoPartial");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
