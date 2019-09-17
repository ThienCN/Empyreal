using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IProvinceService
    {
        List<Province> GetAll();
        Province GetById(int provinceID);
    }
}
