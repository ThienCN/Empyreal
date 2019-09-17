using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Empyreal.Services.Services
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductPriceService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy giá sản phẩm theo ID ( APK )
        /// </summary>
        /// <param name="id">APK</param>
        /// <returns></returns>
        public ProductPrice GetOne(int id)
        {
            return _unitOfWork.ProductPriceRepository
                .Get(p => p.Id == id);
        }

        /// <summary>
        /// Chỉnh sửa ProductPrice
        /// </summary>
        /// <param name="productPrice">ProductPrice</param>
        /// <returns></returns>
        public int Update(ProductPrice productPrice )
        {
            try
            {
                int Result = 0;

                _unitOfWork.ProductPriceRepository.Update(productPrice);

                Result = _unitOfWork.Commit();
                // Commit transaction
                return Result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }
    }
}
