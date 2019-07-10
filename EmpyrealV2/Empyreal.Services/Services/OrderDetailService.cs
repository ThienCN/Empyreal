using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;

namespace Empyreal.Services.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Dictionary<int, string> AddOrderDetail(Order order)
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            int result = 0;

            try
            {
                foreach (var item in order.OrderDetail)
                {
                    _unitOfWork.OrderDetailRepository.Add(item);                    
                }

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

        public void ExecStoreUpdate(string query, params object[] parameters)
        {
            _unitOfWork.OrderDetailRepository.ExecWithStoreProcedureUpdate(query, parameters);
        }
    }
}
