using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface IWardService
    {
        List<Ward> GetAll();
        List<Ward> Get(int districtID);
        Ward GetById(int wardID);
    }
}
