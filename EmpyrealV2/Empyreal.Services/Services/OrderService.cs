using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Create(Order order)
        {
            try
            {
                int result = 0;

                _unitOfWork.OrderRepository.Add(order);                
                result = _unitOfWork.Commit();
                // Commit transaction
                if (result > 0)
                {
                    return order.Id; // => Return ID of new order
                }
                return 0; // => Lỗi
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }

        public Order Get(int orderID)
        {
            return _unitOfWork.OrderRepository.Get(o => o.Id == orderID);
        }

        public Order GetOrderOfUser(string userID)
        {
            return _unitOfWork.OrderRepository
                .Where(o => o.UserId == userID)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();
        }
    }
}
