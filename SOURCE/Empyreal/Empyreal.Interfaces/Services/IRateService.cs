using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IRateService
    {
        List<Rate> GetRatesOfProduct(int productID);
        List<Rate> GetRatePercent(string query, params object[] parameters);
    }
}
