using System;
using System.Collections.Generic;
using System.Text;
using Empyreal.Models;

namespace Empyreal.Interfaces.Services
{
    public interface IProductPriceService
    {
        /// <summary>
        /// Lấy Giá sản phẩm theo Mã APK
        /// </summary>
        /// <param name="id">Mã </param>
        /// <returns></returns>
        ProductPrice GetOne(int id);
        /// <summary>
        /// Chỉnh sửa ProductPrice
        /// </summary>
        /// <param name="productPrice">ProductPrice</param>
        /// <returns> return 1= Success || 0 = Error</returns>
        int Update(ProductPrice productPrice);
    }
}
