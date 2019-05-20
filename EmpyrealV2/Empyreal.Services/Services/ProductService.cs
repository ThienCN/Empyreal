using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Empyreal.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService()
        {
            _unitOfWork = ServiceLocators.ServiceLocator.Current.GetInstance<IUnitOfWork>();
        }

        public void RollBack()
        {
            _unitOfWork.Rollback();
        }

        /// <summary>
        /// Lấy sản phẩm theo Danh mục
        /// </summary>
        /// <param name="catalogID">Mã danh mục</param>
        /// <param name="state">Trạng thái: 0 = Đã bị xóa </param>
        /// <returns>List</returns>
        public List<Product> ByName(string name, int state)
        {
            string nameLower = name.ToLower();
            return _unitOfWork.ProductRepository
                .Where(p => p.Name.ToLower().Contains(nameLower) && p.State == state)
                .ToList();
        }
        /// <summary>
        /// Lấy sản phẩm theo Danh mục và Tên sản phảm ( Tìm theo tên sp gần đúng )
        /// </summary>
        /// <param name="name">Tên sản phẩm</param>
        /// <param name="catalogID">Mã danh mục</param>
        /// <param name="state">Trạng thái: 0 = Đã bị xóa </param>
        /// <returns>List</returns>
        public List<Product> ByNameAndCatalog(string name, int catalogID, int state)
        {
            string nameLower = name.ToLower();
            return _unitOfWork.ProductRepository
                .Where(p => p.CatalogId == catalogID  && p.State == state && p.Name.ToLower().Contains(nameLower))
                .ToList();
        }

        /// <summary>
        /// Lấy 1 sản phảm
        /// </summary>
        /// <param name="ProductID">Mã sản phẩm</param>
        /// <returns>Product</returns>
        public Product Get(int id)
        {
            return _unitOfWork.ProductRepository
                .Get(p => p.Id == id && p.State == 1);
        }

        /// <summary>
        /// Lấy nhiều sản phẩm
        /// </summary>
        /// <param name="ProductID">Mã sản phẩm</param>
        /// <returns></returns>
        public List<Product> GetList(Func<Product, object> TProduct, int Top)
        {
            return _unitOfWork.ProductRepository.Where(p => p.State == 1)
                .OrderBy(TProduct)
                .Take(Top)
                .ToList();
        }

        /// <summary>
        /// Thêm mới sản phẩm
        /// </summary>
        /// <param name="product">Product Entity</param>
        /// <param name="productDetails">List ProductDetail Entity</param>
        /// <param name="images">List Image Entity</param>
        /// <returns>
        /// return the number of rows affected
        /// 0 = Error
        /// </returns>
        public int Create(Product product)
        {
            
            try
            {
                int Result = 0;

                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.ProductRepository.Save();

                foreach (var item in product.ProductDetails)
                {
                    int productID = item.Id;
                    ProductPrice priceNav = item.PriceNavigation;
                    priceNav.ProductDetailId = productID;
                    _unitOfWork.ProductPriceRepository.Update(priceNav);

                    // Commit transaction
                }

                Result = _unitOfWork.Commit();
                // Commit transaction
                return Result;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }

        /// <summary>
        /// Chỉnh sửa sản phẩm
        /// </summary>
        /// <param name="product">Update Product Entity</param>
        /// <param name="productDetails">Update ProductDetail Entity</param>
        /// <param name="images">Update Image Entity</param>
        /// <param name="noUpdateDetail">Create New ProductDetail Entity</param>
        /// <returns>
        /// return the number of rows affected
        /// 0 = Error
        /// </returns>
        public int Update(Product product)
        {
            try
            {
                int isReturn = 0;
                _unitOfWork.ProductRepository.Update(product);
                foreach (var item in product.ProductDetails)
                {
                    int productID = item.Id;
                    ProductPrice priceNav = item.PriceNavigation;
                    priceNav.ProductDetailId = productID;
                    _unitOfWork.ProductPriceRepository.Update(priceNav);

                    // Commit transaction
                }

                isReturn = _unitOfWork.Commit();

                return isReturn;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                return 0; // => Lỗi
            }
        }
    }
}
