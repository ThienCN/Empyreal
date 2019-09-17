using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.Services.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductTypeService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Danh sách loại sản phẩm
        /// </summary>
        /// <param name="TypeName">Tên loại sản phẩm<param>
        /// <param name="State">Trạng thái:  1 = Sử dụng || 0 = Đã bị xóa </param>
        /// <returns>List</returns>
        public List<ProductType> GetProductType(string TypeName, int State)
        {
            return _unitOfWork.ProductTypeRepository
                .Where(t => t.Type == TypeName && t.State == State)
                .OrderBy(order => order.Text)
                .ToList();
        }

        public List<ProductType> GetSizeColor(int sizeID, int colorID)
        {
            return _unitOfWork.ProductTypeRepository
                .Where(t => t.Id == sizeID || t.Id == colorID)
                .ToList();
        }

        public ProductType GetOne(int id)
        {
            return _unitOfWork.ProductTypeRepository
                .Get(t => t.Id == id);
        }
    }
}
