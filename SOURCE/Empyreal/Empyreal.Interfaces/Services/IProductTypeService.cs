using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IProductTypeService
    {
        List<ProductType> GetProductType(string Type, int State);
        List<ProductType> GetSizeColor(int sizeID, int colorID);
        ProductType GetOne(int id);
    }
}
