using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empyreal.Models;
using Empyreal.ViewModels;
using Empyreal.ServiceLocators;
using Empyreal.Interfaces.Services;
using Empyreal.ViewModels.Display;

namespace Empyreal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IImageService imageService;

        private const string TOP_PRODUCT = "Top";
        private const string NEW_PRODUCT = "New";
        private const string RAND_PRODUCT = "Random";

        public HomeController()
        {
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
        }
        public IActionResult Index()
        {
            var listTopProdcuts = GetListProductViewModel(TOP_PRODUCT);
            var listNewProdcuts = GetListProductViewModel(NEW_PRODUCT);
            var listYourProdcuts = GetListProductViewModel(RAND_PRODUCT);
            HomeViewModel homeView = new HomeViewModel()
            {
                TopProducts = listTopProdcuts,
                NewProdcuts = listNewProdcuts,
                YourProducts = listYourProdcuts
            };

            return View(homeView);
        }

        public List<ProductBasicViewModel> GetListProductViewModel(string Mode)
        {
            List<ProductBasicViewModel> ListProductViewModel = new List<ProductBasicViewModel>();
            List<Product> products = new List<Product>();
            // Select Mode

            if (Mode == TOP_PRODUCT)
            {
                products = productService.GetList(a => a.LastModifyDate, 12);
            }   /// Product Top
                       
            else if (Mode == NEW_PRODUCT)
            {
                products = productService.GetList(a => a.CreateDate, 12);
            }   ///  Product New
              
            else
            {
                products = productService.GetList(a => a.Name, 12);
            }   /// Product Random
            
            if (products.Count == 0) return ListProductViewModel;

            //Xử lý Product
            foreach (var product in products)
            {
                // Product Detail
                List<ProductDetail> listDetailProduct = productDetailService.GetList(product.Id);
                ProductDetail detailProduct = new ProductDetail();
                if(listDetailProduct.Count > 0 )
                    listDetailProduct.First();
                // Xử lý khi Detail null
                double? price = double.NaN; // Add to ViewModel
                if (detailProduct != null)
                {
                    price = detailProduct.Price;
                }

                // Image
                Image image = imageService.Get(product.Id);
                ImageBasicViewModel imageViewModel = new ImageBasicViewModel(); // Add to ViewModel
                // Xử lý khi Image null
                if (image != null)
                    imageViewModel.SetURL = image.Url;

                List<ImageBasicViewModel> imagesTemp = new List<ImageBasicViewModel>();
                imagesTemp.Add(imageViewModel);

                ProductBasicViewModel productViewModel = new ProductBasicViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    //UserName
                    //CatalogName
                    PriceProduct = price,
                    Images = imagesTemp
                };

                // Add product into ViewModel
                ListProductViewModel.Add(productViewModel);
                
            }
            return ListProductViewModel;
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
