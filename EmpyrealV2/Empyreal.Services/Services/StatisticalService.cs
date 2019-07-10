using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Statistical> GetAll(string query, params object[] parameters)
        {
            return _unitOfWork.StatisticalRepository.ExecWithStoreProcedure(query, parameters).ToList();
        }
    }
}
