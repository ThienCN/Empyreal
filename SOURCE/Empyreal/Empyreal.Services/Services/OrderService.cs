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

        public IEnumerable<Order> GetList(List<string> FindByUser, string OrderID)
        {
            HashSet<Order> result = new HashSet<Order>();
            if (FindByUser.Count > 0) {
                foreach (var FindUser in FindByUser)
                {
                    var listTemp = _unitOfWork.OrderRepository
                    .Where(user => user.UserId.ToLower().Contains(FindUser.ToLower()) || user.Shipper.ToLower().Contains(FindUser.ToLower()));
                    foreach (var temp in listTemp)
                    {
                        result.Add(temp);
                    }

                }
            }
            else
            {
                var listTemp = _unitOfWork.OrderRepository
                    .Where(order => order.Id.ToString().Contains(OrderID.ToLower()));
                foreach (var temp in listTemp)
                {
                    result.Add(temp);
                }
            }

            return result.OrderByDescending(sort => sort.CreateDate).AsEnumerable();
        }
        public IEnumerable<Order> GetListForShipper(string userID, List<string> FindByUser, string OrderID)
        {
            HashSet<Order> result = new HashSet<Order>();
            if (FindByUser.Count > 0)
            {
                foreach (var FindUser in FindByUser)
                {
                    var listTemp = _unitOfWork.OrderRepository
                    .Where(user => user.Shipper == userID && user.State != 1 && user.UserId.ToLower().Contains(FindUser.ToLower()));
                    foreach (var temp in listTemp)
                    {
                        result.Add(temp);
                    }

                }
            }
            else
            {
                var listTemp = _unitOfWork.OrderRepository
                    .Where(order => order.State != 1 && order.Id.ToString().Contains(OrderID.ToLower()));
                foreach (var temp in listTemp)
                {
                    result.Add(temp);
                }
            }

            return result.OrderByDescending(sort => sort.CreateDate).AsEnumerable();
        }
        public int UpdateOrder(Order order)
        {
            try
            {
                int isReturn = 0;
                order.LastModifyDate = DateTime.Now;
                _unitOfWork.OrderRepository.Update(order);

                isReturn = _unitOfWork.Commit();

                return isReturn;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }

        public List<Order> GetOrders(string userId)
        {
            return _unitOfWork.OrderRepository
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreateDate)
                .ToList();
        }
    }
}
