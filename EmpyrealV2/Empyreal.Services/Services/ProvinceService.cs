using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProvinceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Province> GetAll()
        {
            return _unitOfWork.ProvinceRepository.GetAll().ToList();
        }

        public Province GetById(int provinceID)
        {
            return _unitOfWork.ProvinceRepository.Get(p => p.Id == provinceID);
        }
    }
}
