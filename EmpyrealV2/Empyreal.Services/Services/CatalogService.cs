using Empyreal.Interfaces;
using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using Empyreal.Models;
using System;

namespace Empyreal.Services.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public List<Catalog> AllCatalog()
        {
            return _unitOfWork.CatalogRepository.GetAll().ToList();
        }

        public List<Catalog> GetAll(int state)
        {
            return _unitOfWork.CatalogRepository
                .Where(c => c.State == state)
                .ToList();
        }
        public Catalog Get(int id)
        {
            return _unitOfWork.CatalogRepository.Get(c => c.Id == id && c.State == 1);
        }
    }
}
