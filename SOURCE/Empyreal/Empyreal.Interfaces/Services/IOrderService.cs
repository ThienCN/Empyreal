using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IOrderService
    {
        Order GetOrderOfUser(string userID);
        Order Get(int orderID);
        Dictionary<int, string> Create(Order order);
        List<Order> GetOrders(string userId);
        IEnumerable<Order> GetList(List<string> FindByUser, string OrderID);
        IEnumerable<Order> GetListForShipper(string userID, List<string> FindByUser, string OrderID);
        int UpdateOrder(Order order);
    }
}
