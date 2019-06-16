using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface ICartDetailService
    {
        List<CartDetail> GetAll(int id);
        CartDetail Get(int id);

        int Create(CartDetail cartDetail);
        int Update(CartDetail cartDetail);
    }
}
