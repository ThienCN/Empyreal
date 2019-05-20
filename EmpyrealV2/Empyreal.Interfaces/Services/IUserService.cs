using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface IUserService
    {
        List<User> GetList(Func<User, object> T, int Count);

        List<User> AllUser(string name);

        User Get(string id);
    }
}
