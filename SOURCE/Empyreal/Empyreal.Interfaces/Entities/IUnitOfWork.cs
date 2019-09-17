using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Interfaces.Entities
{
    public interface IUnitOfWork
    {
        int Commit();
        void Rollback();

        #region Repositories
        IRepository<Cart> CartRepository { get; }
        IRepository<CartDetail> CartDetailRepository { get; }
        IRepository<Catalog> CatalogRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderDetail> OrderDetailRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<ProductDetail> ProductDetailRepository { get; }
        IRepository<ProductType> ProductTypeRepository { get; }
        IRepository<ProductReview> ProductReviewRepository { get; }
        IRepository<ProductPrice> ProductPriceRepository { get; }
        IRepository<History> HistoryRepository { get; }
        IRepository<Image> ImageRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Rate> RateRepository { get; }
        IRepository<Province> ProvinceRepository { get; }
        IRepository<District> DistrictRepository { get; }
        IRepository<Ward> WardRepository { get; }
        IRepository<Statistical> StatisticalRepository { get; }
        //IRepository<CommentDetail> CommentSubRepository { get; }
        #endregion Repositories
    }
}
