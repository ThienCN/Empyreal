using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IOrderService
    {
        Order GetOrderOfUser(string userID);
        Order Get(int orderID);
        Dictionary<int, string> Create(Order order);
    }
}
