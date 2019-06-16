using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Services.Services
{
    public class WardService : IWardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Ward> Get(int districtID)
        {
            return _unitOfWork.WardRepository
                .Where(w => w.DistrictId == districtID)
                .Select(w => new Ward
                {
                    Id = w.Id,
                    Name = string.Format("{0} {1}", w.Type, w.Name)
                })
                .ToList();
        }

        public List<Ward> GetAll()
        {
            return _unitOfWork.WardRepository.GetAll().ToList();
        }

        public Ward GetById(int wardID)
        {
            return _unitOfWork.WardRepository.Get(w => w.Id == wardID);
        }
    }
}
