using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Services.Services
{
    public class ProductDetailService : IProductDetailService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductDetailService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Lấy 1 chi tiết sản phảm
        /// </summary>
        /// <param name="detailID">Mã chi tiết sản phẩm</param>
        /// <returns></returns>
        public ProductDetail GetOne(int detailID)
        {
            return _unitOfWork.ProductDetailRepository
                .Get(p => p.Id == detailID && p.State == 1);
        }

        /// <summary>
        /// Lấy nhiều chi tiết sản phẩm
        /// </summary>
        /// <param name="ProductID">Mã sản phẩm</param>
        /// <returns></returns>
        public List<ProductDetail> GetList(int ProductID)
        {
            return _unitOfWork.ProductDetailRepository
                .Where(p => p.ProductId == ProductID && p.State == 1)
                .ToList();
        }

    }
}
