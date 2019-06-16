using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Services.Services
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CommonModel> GetRatePercents(string query, params object[] parameters)
        {
            return _unitOfWork.CommonRepository.ExecWithStoreProcedure(query, parameters).ToList();
        }
    }
}
