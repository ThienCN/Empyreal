using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Services.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductReviewService()
        {
            _unitOfWork = ServiceLocators.ServiceLocator.Current.GetInstance<IUnitOfWork>();
        }

        public int UpdateView(int productID)
        {
            ProductReview review = _unitOfWork.ProductReviewRepository.Get(r => r.ProductID == productID);
            if(review == null)
            { // Tạo mới
                review = new ProductReview();
                review.ProductID = productID;
            }
            else
            { // Chỉnh sửa => Tăng View +1
                review.View++;
            }
            _unitOfWork.ProductReviewRepository.Update(review);
            return _unitOfWork.Commit();
        }
    }
}
