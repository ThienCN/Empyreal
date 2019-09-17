using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DistrictService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<District> Get(int provinceID)
        {
            return _unitOfWork.DistrictRepository
                .Where(d => d.ProvinceId == provinceID)
                .Select(d => new District
                {
                    Id = d.Id,
                    Name = string.Format("{0} {1}", d.Type, d.Name)
                })
                .ToList();
        }

        public List<District> GetAll()
        {
            return _unitOfWork.DistrictRepository.GetAll().ToList();
        }

        public District GetById(int districtID)
        {
            return _unitOfWork.DistrictRepository.Get(d => d.Id == districtID);
        }
    }
}
