using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class RateService : IRateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Rate> GetRatePercent(string query, params object[] parameters)
        {
            return _unitOfWork.RateRepository.ExecWithStoreProcedure(query, parameters).ToList();
        }

        public List<Rate> GetRatesOfProduct(int productID)
        {
            return _unitOfWork.RateRepository.Where(pID => pID.ProductId == productID).ToList();
        }
    }
}
