using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface IProductTypeService
    {
        List<ProductType> GetProductType(string Type, int State);
    }
}
