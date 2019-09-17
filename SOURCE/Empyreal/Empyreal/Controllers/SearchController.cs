using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels;
using Empyreal.ViewModels.Display;
using Empyreal.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;

namespace Empyreal.Controllers
{
    public class SearchController : Controller
    {
        #region --- Variables ---
        // Service
        private readonly ICatalogService catalogService;
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductPriceService priceService;
        private readonly IHistoryService historyService;

        private readonly IImageService imageService;
        private readonly IUserService userService;



        #endregion --- Variables ---

        #region --- Constructor ---

        public SearchController()
        {
            catalogService = ServiceLocator.Current.GetInstance<ICatalogService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            historyService = ServiceLocator.Current.GetInstance<IHistoryService>();
        }

        #endregion --- Constructor ---

        #region --- Request ---


        public IActionResult Index(string KeySearch)
        {
            Product product = productService.GetByName(KeySearch);
            if (product != null)
            {
                var productID = product.Id;
                return RedirectToAction("Product", "Home", new { productID = productID });
            }
            ViewData["Title"] = KeySearch;
            return View();
        }

        public async Task<IActionResult> ProductSearch(string KeySearch)
        {
            try
            {
                string input = KeySearch.ToLower();
                // Lấy 10 sản phẩm gần giống
                List<Product> productList = productService.GetAvailable()
                    .Where(p => p.Name.ToLower().Contains(input))
                    .Take(10).ToList();
                List<ProductSearchViewModel> viewModelList = new List<ProductSearchViewModel>();
                foreach (var item in productList)
                {
                    var image = imageService.Get(item.Id);
                    ProductSearchViewModel vmodel = new ProductSearchViewModel()
                    {
                        Name = item.Name,
                        Url = image.Url
                    };
                    viewModelList.Add(vmodel);
                }
                return Ok(viewModelList);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Tiếp nhận Request & Lấy dữ liệu lịch sử
        /// </summary>
        /// <param name="detail"> ID of Table.Row</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSearchPartial(string KeySearch, int page = 1, int pageSize = 20)
        {
            if (String.IsNullOrEmpty(KeySearch))
                KeySearch = string.Empty;
            var dataVModel = GetListProductViewModel(KeySearch);
            PagedSearchViewModel VModel = new PagedSearchViewModel(dataVModel, page, pageSize);
            return PartialView("Product\\_ProductSearch", VModel);

        }
        #endregion --- Request ---


        #region --- Public Method ---


        public IEnumerable<ProductBasicViewModel> GetListProductViewModel(string KeySearch)
        {
            List<string> listKey = KeySearch.Split(" ").ToList();
            string key = KeySearch.Trim();
            int indexSpace = KeySearch.LastIndexOf(" ");
            HashSet<Product> products = new HashSet<Product>();

            if(indexSpace == -1)
            { // String not contain Space
                List<Product> listItem = new List<Product>();
                listItem = productService.GetAvailable()
               .Where(p => p.Name.ToLower().Contains(key.ToLower()))
               .OrderByDescending(a => a.ProductReivews.FirstOrDefault().View)
               .ToList();

                foreach (var item in listItem)
                {
                    products.Add(item);
                }
            }

            while (indexSpace > -1)
            {
                key = key.Substring(0, indexSpace);

                indexSpace = key.LastIndexOf(" ");

                List<Product> listItem = new List<Product>();
                listItem = productService.GetAvailable()
               .Where(p => p.Name.ToLower().Contains(key.ToLower()))
               .OrderByDescending(a => a.ProductReivews.FirstOrDefault().View)
               .ToList();

                foreach (var item in listItem)
                {
                    products.Add(item);

                }

            }
            //foreach (var key in listKey)
            //{
            //    List<Product> listItem = new List<Product>();
            //    listItem = productService.GetAvailable()
            //   .Where(p => p.Name.ToLower().Contains(key.ToLower()))
            //   .OrderByDescending(a => a.ProductReivews.FirstOrDefault().View)
            //   .Take(12)
            //   .ToList();

            //    foreach (var item in listItem)
            //    {
            //        products.Add(item);

            //    }
            //}
            // Select Mode


            if (products.Count() == 0) return new List<ProductBasicViewModel>();

            IEnumerable<ProductBasicViewModel> VModel = MappingVModel(products);
            return VModel;
        }


        public IEnumerable<ProductBasicViewModel> MappingVModel(IEnumerable<Product> products)
        {
            List<ProductBasicViewModel> ListProductViewModel = new List<ProductBasicViewModel>();

            //Xử lý Product
            foreach (var product in products)
            {
                // Product Detail
                List<ProductDetail> listDetailProduct = productDetailService.GetList(product.Id);
                if (listDetailProduct == null || listDetailProduct.Count == 0)
                    continue;
                ProductDetail detailProduct = listDetailProduct.FirstOrDefault();

                ProductPrice priceNav = new ProductPrice();
                priceNav = priceService.GetOne(detailProduct.Price.GetValueOrDefault());
                // Xử lý khi Detail null
                double? price = double.NaN; // Add to ViewModel
                if (detailProduct != null)
                {
                    price = priceNav.Price;
                }

                // Image
                Image image = imageService.Get(product.Id);
                ImageBasicViewModel imageViewModel = new ImageBasicViewModel(); // Add to ViewModel
                // Xử lý khi Image null
                if (image != null)
                    imageViewModel.SetURL = image.Url;

                List<ImageBasicViewModel> imagesTemp = new List<ImageBasicViewModel>
                {
                    imageViewModel
                };

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
            return ListProductViewModel.AsEnumerable();
        }

        #endregion --- Public Method ---

    }
}