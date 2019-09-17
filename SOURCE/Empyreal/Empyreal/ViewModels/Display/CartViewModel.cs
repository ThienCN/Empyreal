using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.ViewModels.Display
{
    public class CartViewModel
    {
        public List<CartDetailViewModel> Cart { get; set; }

        public CartViewModel()
        {
            Cart = new List<CartDetailViewModel>();
        }
    }

    public class CartDetailViewModel
    {
        public ProductBasicViewModel Product { get; set; }
        public ProductDetailBasicViewModel ProductDetail { get; set; }
        public string ImageURL { get; set; }
        public int CartDetailId { get; set; }
        public int? BuyedQuantity { get; set; }

        public CartDetailViewModel(Product product, ProductDetail productDetail,
            string imageURL, int cartDetailId, int buyedQuantity)
        {
            // Product
            Product = new ProductBasicViewModel(product);

            // Product detail
            ProductDetail = new ProductDetailBasicViewModel(productDetail);

            // Image URL
            ImageURL = imageURL;

            CartDetailId = cartDetailId;

            BuyedQuantity = buyedQuantity;
        }

        public CartDetailViewModel() { }
    }
}
