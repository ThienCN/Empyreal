using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface IProductService
    {
        void RollBack();
        Product Get(int id);
        List<Product> GetList(Func<Product, object> T, int Count);
        List<Product> ByName(string name, int state);
        List<Product> ByNameAndCatalog(string name, int catalogID, int state);

        /// <summary>
        /// Thêm mới sản phẩm:
        /// Return the Number of Rows Affected
        /// </summary>
        /// <param name="product">Product Entity</param>
        /// <param name="productDetails">List ProductDetail Entity</param>
        /// <param name="images">List Image Entity</param>
        /// <returns>
        /// 0 = Error
        /// </returns>
        int Create(Product product);
        int Update(Product product);

    }
}
    