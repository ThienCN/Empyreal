using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy nhiều sản phẩm
        /// </summary>
        /// <param name="ProductID">Mã sản phẩm</param>
        /// <returns></returns>
        public List<Product> GetList(Func<Product, object> TProduct, int Top)
        {
            return _unitOfWork.ProductRepository.Where(p => p.State == 1)
                .OrderBy(TProduct)
                .Take(Top)
                .ToList();
        }

        public List<User> AllUser(string name)
        {
            string keySearch = name.ToLower();
            return _unitOfWork.UserRepository
                .Where(u => u.HoTen.ToLower().Contains(keySearch))
                .ToList();
        }

        public User Get(string id)
        {
            return _unitOfWork.UserRepository
                .Get(u => u.Id == id);
        }

        public List<User> GetList(Func<User, object> T, int Count)
        {
            throw new NotImplementedException();
        }

        public int Update(User user)
        {
            try
            {
                int result = 0;

                _unitOfWork.UserRepository.Update(user);

                result = _unitOfWork.Commit();
                // Commit transaction
                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }
    }
}
