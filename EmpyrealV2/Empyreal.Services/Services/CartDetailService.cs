using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class CartDetailService : ICartDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Create(CartDetail cartDetail)
        {
            int result = 0;
            try
            {
                _unitOfWork.CartDetailRepository.Add(cartDetail);
                result = _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // Error
            }
        }

        public CartDetail Get(int id)
        {
            return _unitOfWork.CartDetailRepository.Get(cd => cd.Id == id);
        }

        // Get all product of cart
        public List<CartDetail> GetAll(int id)
        {
            return _unitOfWork.CartDetailRepository
                .GetAll()
                .Where(cd => cd.CartId == id && cd.State == 1)
                .ToList();
        }

        public int Update(CartDetail cartDetail)
        {
            int result = 0;
            try
            {
                _unitOfWork.CartDetailRepository.Update(cartDetail);
                result = _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // Error
            }
        }
    }
}
