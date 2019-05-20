using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface IProductDetailService
    {
        ProductDetail GetOne(int detailID);
        List<ProductDetail> GetList(int ProductID);
    }
}
