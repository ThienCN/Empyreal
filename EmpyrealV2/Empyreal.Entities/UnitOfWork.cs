using Empyreal.Interfaces.Entities;
using Empyreal.Models;
using Empyreal.Models.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empyreal.Entities
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Variables

        private readonly EmpyrealContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        #region DbSet

        private IRepository<Cart> _cartRepository;
        private IRepository<CartDetail> _cartDetailRepository;
        private IRepository<Catalog> _catalogRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<OrderDetail> _orderDetailRepository;
        private IRepository<Product> _productRepository;
        private IRepository<User> _userRepository;
        private IRepository<ProductDetail> _productDetailRepository;
        private IRepository<ProductType> _productTypeRepository;
        private IRepository<ProductPrice> _productPriceRepository;

        private IRepository<Image> _imageRepository;
        private IRepository<Comment> _commentRepository;
        private IRepository<Rate> _rateRepository;
        private IRepository<CommonModel> _commonRepository;
        private IRepository<Province> _provinceRepository;
        private IRepository<District> _districtRepository;
        private IRepository<Ward> _wardRepository;

        #endregion DbSet

        #endregion Variables

        #region Constructor

        public UnitOfWork(EmpyrealContext context, IDbContextTransaction transaction = null)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        #endregion Constructor

        #region SetRepository
        public IRepository<Cart> CartRepository
        {
            get
            {
                return _cartRepository = _cartRepository ?? new Repository<Cart>(_context);
            }
        }
        public IRepository<CartDetail> CartDetailRepository
        {
            get
            {
                return _cartDetailRepository = _cartDetailRepository ?? new Repository<CartDetail>(_context);
            }
        }
        public IRepository<Catalog> CatalogRepository
        {
            get
            {
                return _catalogRepository = _catalogRepository ?? new Repository<Catalog>(_context);
            }
        }
        public IRepository<Order> OrderRepository
        {
            get
            {
                return _orderRepository = _orderRepository ?? new Repository<Order>(_context);
            }
        }
        public IRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository = _orderDetailRepository ?? new Repository<OrderDetail>(_context);
            }
        }
        public IRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository = _productRepository ?? new Repository<Product>(_context);
            }
        }
        public IRepository<User> UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new Repository<User>(_context);
            }
        }
        public IRepository<ProductDetail> ProductDetailRepository
        {
            get
            {
                return _productDetailRepository = _productDetailRepository ?? new Repository<ProductDetail>(_context);
            }
        }
        public IRepository<ProductType> ProductTypeRepository
        {
            get
            {
                return _productTypeRepository = _productTypeRepository ?? new Repository<ProductType>(_context);
            }
        }
        public IRepository<ProductPrice> ProductPriceRepository
        {
            get
            {
                return _productPriceRepository = _productPriceRepository ?? new Repository<ProductPrice>(_context);
            }
        }
        public IRepository<Image> ImageRepository
        {
            get
            {
                return _imageRepository = _imageRepository ?? new Repository<Image>(_context);
            }
        }
        public IRepository<Comment> CommentRepository
        {
            get
            {
                return _commentRepository = _commentRepository ?? new Repository<Comment>(_context);
            }
        }
        public IRepository<Rate> RateRepository
        {
            get
            {
                return _rateRepository = _rateRepository ?? new Repository<Rate>(_context);
            }
        }

        public IRepository<CommonModel> CommonRepository
        {
            get
            {
                return _commonRepository = _commonRepository ?? new Repository<CommonModel>(_context);
            }
        }

        public IRepository<Province> ProvinceRepository
        {
            get
            {
                return _provinceRepository = _provinceRepository ?? new Repository<Province>(_context);
            }
        }

        public IRepository<District> DistrictRepository
        {
            get
            {
                return _districtRepository = _districtRepository ?? new Repository<District>(_context);
            }
        }

        public IRepository<Ward> WardRepository
        {
            get
            {
                return _wardRepository = _wardRepository ?? new Repository<Ward>(_context);
            }
        }


        #endregion SetRepository

        #region Public Method

        #region MainExecute

        /// <summary>
        /// Return the Number of Rows Affected
        /// </summary>
        public int Commit()
        {
            var transaction = _transaction != null ? _transaction : _context.Database.BeginTransaction();
            using (transaction)
            {
                try
                {
                    //Context boş ise hata fırlatıyoruz
                    if (_context == null)
                    {
                        throw new ArgumentException("Context is null");
                    }
                    int result = _context.SaveChanges();

                    transaction.Commit();
                    //

                    _transaction = null;

                    return result;
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    throw new Exception("Error on save changes ", ex);
                }
            }
        }

        /// <summary>
        /// Roll Back & Reload entity
        /// </summary>
        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }

        #endregion MainExecute

        #region Transaction

        /// <summary>
        /// Start Transaction
        /// </summary>
        /// <returns></returns>
        public bool BeginNewTransaction()
        {
            try
            {
                _transaction = _context.Database.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Roll Back Transaction
        /// </summary>
        /// <returns></returns>
        public bool RollBackTransaction()
        {
            try
            {
                _transaction.Rollback();
                _transaction = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion Transaction

        #region DisposingSection

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion DisposingSection

        #endregion Public Method

    }
}
