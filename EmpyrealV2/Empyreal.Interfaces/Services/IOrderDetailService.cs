using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IOrderDetailService
    {
        Dictionary<int, string> AddOrderDetail(Order order);
        void ExecStoreUpdate(string query, params object[] parameters);
    }
}
