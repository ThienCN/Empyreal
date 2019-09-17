using Empyreal.Models;

namespace Empyreal.Interfaces.Services
{
    public interface ICartService
    {
        Cart Get(string id);
        int Create(Cart cart);
    }
}
