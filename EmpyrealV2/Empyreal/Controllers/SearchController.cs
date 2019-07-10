using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels;
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


        #region --- Public Method ---

        public async Task<IActionResult> ProductSearch(string KeySeach)
        {
            try
            {
                string input = KeySeach.ToLower();
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


        #endregion --- Public Method ---

    }
}