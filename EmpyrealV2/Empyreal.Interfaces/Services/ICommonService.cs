using Empyreal.Models.BaseModel;
using System.Collections.Generic;

namespace Empyreal.Interfaces.Services
{
    public interface ICommonService
    {
        List<CommonModel> GetRatePercents(string query, params object[] parameters);
    }
}
