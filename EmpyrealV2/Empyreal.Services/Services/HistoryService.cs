using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Services.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HistoryService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public List<History> GetByDetail(string table, int detail)
        {
            return _unitOfWork.HistoryRepository
                .Where( h => h.Table == table && h.Detail == detail)
                .OrderByDescending( h => h.CreateDate )
                .ToList();
        }


        public List<History> GetByTable(string table)
        {
            return _unitOfWork.HistoryRepository
                .Where(h => h.Table == table)
                .OrderByDescending(h => h.CreateDate)
                .ToList();
        }


        public int SaveHistory(History history)
        {
            try
            {
                int isReturn = 0;
                _unitOfWork.HistoryRepository.Update(history);
               
                isReturn = _unitOfWork.Commit();

                return isReturn;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }
    }
}
