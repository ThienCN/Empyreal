using Empyreal.Models;
using System;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IProductService
    {
        void RollBack();
        Product Get(int id);
        Product GetByName(string name);

        List<Product> GetList(Func<Product, object> T, int Count);

        /// <summary>
        /// Thêm mới sản phẩm:
        /// Return the Number of Rows Affected
        /// </summary>
        /// <param name="product">Product Entity</param>
        /// <param name="productDetails">List ProductDetail Entity</param>
        /// <param name="images">List Image Entity</param>
        /// <param name="history">Save History</param>
        /// <returns>
        /// 0 = Error
        /// </returns>
        int Update(Product product, History history);
        int Delete(Product product, History history);

        /// <summary>
        /// Search full text ( LIKE )
        /// </summary>
        /// <param name="text"></param>
        /// <param name="catalogID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        List<Product> SearchFullText(string text, int catalogID, int state);


        IEnumerable<Product> GetAvailable();

        List<Product> StatisticalBestSeller(string query);
    }
}
    