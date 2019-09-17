using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IDistrictService
    {
        List<District> GetAll();
        List<District> Get(int provinceID);
        District GetById(int districtID);
    }
}
