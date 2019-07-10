using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Mvc;

namespace Empyreal.Controllers
{
    public class ProductPartialController : Controller
    {
        private const string TOP_PRODUCT = "Top";
        private const string NEW_PRODUCT = "New";
        private const string RAND_PRODUCT = "Random";

        IProductService productService = ServiceLocator.Current.GetInstance<IProductService>();
        IProductDetailService productDetailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
        IProductPriceService priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
        IImageService imageService = ServiceLocator.Current.GetInstance<IImageService>();

        
        [HttpGet]
        public IActionResult CreateProductView(string mode)
        {
            IEnumerable<ProductBasicViewModel> products = GetListProductViewModel(mode);
            string View = String.Empty;
            if(mode == TOP_PRODUCT)
            {
                View = "Product\\_ProductTop";
            }
            else if (mode == NEW_PRODUCT)
            {
                View = "Product\\_ProductNew";
            }
            else if (mode == RAND_PRODUCT)
            {
                View = "Product\\_ProductRandom";

            }
            return PartialView(View, products);
        }
        public IEnumerable<ProductBasicViewModel> GetListProductViewModel(string mode)
        {
            IEnumerable<Product> products;
            // Select Mode

            if (mode == TOP_PRODUCT)
            {
                products = productService.GetList(a => a.Name, 12);
            }   /// Product Top

            else if (mode == NEW_PRODUCT)
            {
                products = productService.GetList(a => a.CreateDate, 12).Take(12);
            }   ///  Product New            
            else
            {
                products = productService.GetList(a => Guid.NewGuid(), 12); // Random
            }   /// Product Random

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
                    price = detailProduct.Price;
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

    }
}