using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IStatisticalService
    {
        List<Statistical> GetAll(string query, params object[] parameters);
    }
}
