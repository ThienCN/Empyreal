using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Display
{
    public class CartViewModel
    {
        public List<CartDetailViewModel> Cart { get; set; }
    }

    public class CartDetailViewModel
    {
        public ProductBasicViewModel Product { get; set; }
        public ProductDetailBasicViewModel ProductDetail { get; set; }
        public string ImageURL { get; set; }
        public int CartDetailId { get; set; }

        public CartDetailViewModel(Product product, ProductDetail productDetail,
            string imageURL, int cartDetailId)
        {
            // Product
            Product = new ProductBasicViewModel(product);

            // Product detail
            ProductDetail = new ProductDetailBasicViewModel(productDetail);

            // Image URL
            ImageURL = imageURL;

            CartDetailId = cartDetailId;
        }
    }
}
