using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
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

        public Dictionary<int, string> Create(Order order)
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            int result = 0;

            try
            {
                _unitOfWork.OrderRepository.Add(order);

                // Commit transaction               
                result = _unitOfWork.Commit();

                if (result > 0) keyValuePairs.Add(order.Id, ""); // => Return ID of new order
                else keyValuePairs.Add(0, "Add fail"); // => Lỗi
                return keyValuePairs;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                keyValuePairs.Add(0, e.Message); // => Lỗi
                return keyValuePairs;
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
