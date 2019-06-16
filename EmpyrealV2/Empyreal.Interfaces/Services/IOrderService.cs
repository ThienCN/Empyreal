using Empyreal.Models;

namespace Empyreal.Interfaces.Services
{
    public interface IOrderService
    {
        Order GetOrderOfUser(string userID);
        Order Get(int orderID);
        int Create(Order order);
    }
}
