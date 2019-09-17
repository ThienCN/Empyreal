using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IProductDetailService
    {
        ProductDetail GetOne(int detailID);
        List<ProductDetail> GetList(int ProductID);
        int Update(List<ProductDetail> productDetail);
    }
}
