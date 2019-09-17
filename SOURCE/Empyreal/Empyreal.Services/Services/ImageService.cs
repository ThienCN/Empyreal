using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Services.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImageService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Lấy 1 hình ảnh
        /// </summary>
        /// <param name="ProductID">Mã sản phẩm</param>
        /// <returns></returns>
        public Image Get(int ProductID)
        {
            return _unitOfWork.ImageRepository
                .Where(p => p.ProductId == ProductID && p.State == 1)
                .FirstOrDefault();
        }

        /// <summary>
        /// Lấy nhiều hình ảnh
        /// </summary>
        /// <param name="ProductID">Mã sản phẩm</param>
        /// <returns></returns>
        public List<Image> GetList(int ProductID)
        {
            return _unitOfWork.ImageRepository
                .Where(p => p.ProductId == ProductID && p.State == 1)
                .ToList();
        }

        public Image GetOne(int ImageID)
        {
            return _unitOfWork.ImageRepository.Get(img => img.Id == ImageID);
        }
    }
}
