using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;

namespace Empyreal.Services.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Create(Cart cart)
        {
            int result = 0;
            try
            {
                _unitOfWork.CartRepository.Add(cart);
                result = _unitOfWork.Commit();

                return result;
            }
            catch (System.Exception)
            {
                _unitOfWork.Rollback();
                return 0; // Error
            }
        }

        public Cart Get(string id)
        {
            return _unitOfWork.CartRepository.Get(c => c.UserId == id);
        }
    }
}
