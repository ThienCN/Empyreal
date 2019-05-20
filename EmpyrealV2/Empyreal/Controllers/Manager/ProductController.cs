using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Empyreal.ViewModels.Manager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using PagedList.Core;

namespace Empyreal.Controllers.Manager
{
    public class ProductController : Controller
    {
        #region --- Variables ---
        // Service
        private readonly ICatalogService catalogService;
        private readonly IProductService productService;
        private readonly IProductDetailService detailService;
        private readonly IProductPriceService priceService;

        private readonly IImageService imageService;
        private readonly IUserService userService;

        // Environment
        private IHostingEnvironment _env;

        // Constant 
        private const int MODE_MANAGER = 0;
        private const int MODE_UPDATE = 1;
        #endregion --- Variables ---

        #region --- Constructor ---

        public ProductController(IHostingEnvironment env)
        {
            _env = env;

            catalogService = ServiceLocator.Current.GetInstance<ICatalogService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            detailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
            userService = ServiceLocator.Current.GetInstance<IUserService>();
        }

        #endregion --- Constructor ---

        #region --- Request ---

        /// <summary>
        /// Màn hình danh mục: Danh sách sản phẩm
        /// </summary>
        /// <param name="keySearch">Từ khóa tìm kiếm</param>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Số sản phảm trong 1 trang</param>
        /// <param name="selectByCatalog">Chọn theo ComboBox Danh mục sản phẩm</param>
        [HttpGet]
        public IActionResult ProductManager(int catalogSelect, string keySearch, int page = 1, int pageSize = 5)
        {
            var Catalogs = new ComboBoxController().GetCatalogs(MODE_MANAGER);
            //
            if (string.IsNullOrWhiteSpace(keySearch))
                keySearch = string.Empty;
            //var user = await userManager.GetUserAsync(User);
            List<Product> products = new List<Product>();
            if (catalogSelect == 0)
                products = productService.ByName(keySearch, 1);
            else
                products = productService.ByNameAndCatalog(keySearch, catalogSelect, 1);
            //
            List<ProductBasicViewModel> listProduct = new List<ProductBasicViewModel>();
            foreach (var p in products)
            {
                string userName = string.Empty;
                //User user = userService.ByID(p.UserId);
                //if (user != null)
                //    userName = user.HoTen;

                ProductBasicViewModel product = new ProductBasicViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    UserName = userName,
                    State = p.State
                };
                listProduct.Add(product);
            }
            //
            ProductManagerViewModel model =
                new ProductManagerViewModel(listProduct, page, pageSize, Catalogs, keySearch, catalogSelect);

            return View(model);
        }

        /// <summary>
        /// Get: Cập nhật sản phẩm
        /// </summary>
        /// <param name="isUpdate"> True: Update || False: Insert</param>
        /// <returns>ProductManagerViewModel</returns>
        /// <history>
        /// [Lương Mỹ] Create [20/04/2019]
        /// </history>
        [HttpGet]
        public IActionResult ProductUpdate(int productID, bool isUpdate)
        {

            ProductUpdateViewModel viewModel = new ProductUpdateViewModel(new Product());
            viewModel.IsUpdate = isUpdate;

            #region --- Default --- 


            if (viewModel.IsUpdate)
                ViewData["Title"] = "Chỉnh sửa sản phẩm";
            else
                ViewData["Title"] = "Thêm mới sản phẩm";

            #endregion --- Default --- 

            string message;
            bool isError;
            Product product;
            List<ProductDetail> productDetails;
            List<Image> images;
            //
            GetDataFromSever(productID, out product, out productDetails, out images, out isError, out message);
            //
            Mapping(product, productDetails, images, ref viewModel);

            InitComboBox(ref viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// Post: Cập nhật sản phẩm
        /// </summary>
        /// <param name="viewModel">Dữ liệu nhận về</param>
        /// <returns>View của HTTP: Get</returns>
        /// <history>
        /// [Lương Mỹ] ProductManagerViewModel [20/04/2019]
        /// </history>
        [HttpPost]
        public IActionResult ProductUpdate(ProductUpdateViewModel viewModel)
        {
            int isReturn = 0;
            var message = string.Empty;

            // Input => ViewModel
            ConfigViewModel(ref viewModel);

            // Product Entity
            Product product = null;
            //Excute
            try
            {
                DataTranfer(out product, viewModel);
            }
            catch (Exception e)
            {
                message = e.Message;
                productService.RollBack();
                goto Finish;
            }

            // Call Service => Execute
            #region --- Main Execute ---

            // Execute Insert
            try
            {
                if (!viewModel.IsUpdate) { // Create
                    isReturn = productService.Create(product);
                }
                else // Update
                {
                    isReturn = productService.Update(product);
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                goto Finish;
            }


        Finish:
            // isReturn = 0: Error || 1: Success
            // Giải thích: isReturn = dbContext.Commit = số dòng được thay đổi trong sql
            viewModel.IsError = (isReturn == 0);
            viewModel.Message = message;



            #endregion --- Main Execute ---

            return View(viewModel);
        }

        #endregion --- Request --- 

        #region --- Method ---


        /// <summary>
        /// Khởi tạo ComboBox cho Create / Update
        /// </summary>
        /// <returns>
        /// out viewModel
        /// </returns>
        public void InitComboBox(ref ProductUpdateViewModel viewModel)
        {
            //Get Data ComboBox
            var comboBox = new ComboBoxController();
            viewModel.Catalogs = comboBox.GetCatalogs(MODE_UPDATE);

            viewModel.Sizes = comboBox.GetProductType("Size");
            viewModel.Colors = comboBox.GetProductType("Color");

            return;
        }
        //end InitViewModel

        /// <summary>
        /// Xử lý Input thành ViewModel hoàn chỉnh
        /// </summary>
        /// <param name="viewModel"> ViewModel chứa Input</param>
        /// <history>
        /// [Lương Mỹ] Create [29/04/2019]
        /// </history>
        public void ConfigViewModel (ref ProductUpdateViewModel viewModel)
        {
            // Input => ViewModel
            #region --- Set Value ---
            InitComboBox(ref viewModel);

            if (viewModel.IsUpdate)
                ViewData["Title"] = "Chỉnh sửa sản phẩm";
            else
                ViewData["Title"] = "Thêm mới sản phẩm";


            var DetailID = viewModel.ProductDetailId;
            var Size = viewModel.Size;
            var Color = viewModel.Color;
            var PriceID = viewModel.PriceId;
            var PriceText = viewModel.PriceText;

            var Quantity = viewModel.Quantity;

            #endregion --- Set Value ---
            #region --- Execute ---

            // Add Partial View => View Model
            viewModel.ProductDetails = new List<ProductDetailBasicViewModel>();
            if (DetailID != null & DetailID.Count > 0)
            {
                for (int i = 0; i < DetailID.Count; i++)
                {
                    var productDetail = new ProductDetailBasicViewModel()
                    {
                        ID = DetailID[i],
                        Size = Size[i],
                        Color = Color[i],
                        Quantity = Quantity[i],
                        PriceID = PriceID[i],
                    };
                    viewModel.ProductDetails.Add(productDetail);
                };
            }


            // Add Image in View => View Model
            viewModel.Images = new List<ImageBasicViewModel>();
            if (viewModel.Files != null && viewModel.Files.Count > 0)
            {
                // Temp
                List<ImageBasicViewModel> imgs = new List<ImageBasicViewModel>();
                // Main Execute Image
                foreach (var file in viewModel.Files)
                {
                    try
                    {
                        if (file.Length > 0)
                        {
                            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                            string fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                                                                                     //for the file due to security 

                            var webRoot = _env.WebRootPath;
                            string filePath = Path.Combine(webRoot, "images", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            string url = "/images/" + fileName;
                            ImageBasicViewModel img = new ImageBasicViewModel
                            {
                                SetURL = url
                            };
                            imgs.Add(img);

                        }
                    }
                    catch (Exception e)
                    {
                        viewModel.IsError = true;
                        viewModel.Message = e.Message;
                        return ;
                    }

                }
                // Gán lại cho ViewModel
                viewModel.Images = imgs;
            }

            #endregion --- Execute ---

            return;
        }
        //end ConfigViewModel

        /// <summary>
        /// Chuyển đổi từ ViewModel => Entity
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productDetails"></param>
        /// <param name="images"></param>
        /// <param name="viewModel"> ViewModel chứa Input</param>
        /// <history>
        /// [Lương Mỹ] Create [29/04/2019]
        /// </history>
        public void DataTranfer(out Product product, ProductUpdateViewModel viewModel)
        {
            // ViewModel => Entity
            #region --- Tranfer ---

            // Product Entity => Product
            int productID = viewModel.ProductId;
            product = (productID == 0) ? new Product() : productService.Get(productID);

            product.Id = viewModel.ProductId;
            product.Name = viewModel.ProductName;
            //UserID
            product.CatalogId = viewModel.Catalog;
            product.Description = viewModel.Description;
            product.CreateDate = DateTime.Now;
            product.State = 1;

            // ProductDetail Entity => ProductDetails
            var productDetails = new List<ProductDetail>();
            for (int i = 0; i < viewModel.ProductDetails.Count; i++)
            {
                var proDetailModel = viewModel.ProductDetails[i];

                // ProductDetail
                int detailID = proDetailModel.ID;
                ProductDetail detail = (detailID == 0) ? new ProductDetail() : detailService.GetOne(detailID);
                detail.Id = proDetailModel.ID;
                detail.Size = proDetailModel.Size;
                detail.Color = proDetailModel.Color;
                detail.Quantity = proDetailModel.Quantity;
                detail.Price = proDetailModel.PriceID;
                detail.CreateDate = DateTime.Now;
                detail.State = 1;



                // ProductPrice
                int priceID = proDetailModel.PriceID.GetValueOrDefault();

                ProductPrice productPrice = null;
                if (priceID != 0 )
                {
                    productPrice = priceService.GetOne(priceID);
                    // Tạo mới Price nếu Thay đổi giá tiền
                    if (productPrice.Price != viewModel.PriceText[i])
                    {
                        productPrice = new ProductPrice();
                    }
                }
                else
                {
                    productPrice = new ProductPrice();
                }
                productPrice.Price = viewModel.PriceText[i];
                productPrice.State = 1;

                detail.PriceNavigation = productPrice;
                productDetails.Add(detail);

            }//end Entity ProductDetail

            // Image Entity => List<Image>
            var images = new List<Image>();
            for (int i = 0; i < viewModel.Images.Count; i++)
            {
                var imgModel = viewModel.Images[i];

                Image img = new Image()
                {
                    Url = imgModel.Url
                };
                images.Add(img);
            }
            //

            product.Images = images;
            product.ProductDetails = productDetails;
            #endregion --- Tranfer ---

        }
        //end DataTranfer

        /// <summary>
        /// Chuyển đổi từ Entity => ViewModel
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productDetails"></param>
        /// <param name="images"></param>
        /// <param name="viewModel"> ViewModel chứa Input</param>
        /// <history>
        /// [Lương Mỹ] Create [29/04/2019]
        /// </history>
        public void Mapping( Product product, List<ProductDetail> productDetails, List<Image> images,
            ref ProductUpdateViewModel viewModel)
        {
            // Entity => ViewModel
            #region --- Excute ---

            viewModel = new ProductUpdateViewModel(product);

            // ProductDetail Entity => List<ProductDetail>
            for (int i = 0; i < productDetails.Count; i++)
            {
                var proDetailVModel = productDetails[i];
                ProductDetailBasicViewModel proVModel = new ProductDetailBasicViewModel(proDetailVModel);
                viewModel.ProductDetails.Add(proVModel);
            }

            // Image Entity => List<Image>
            for (int i = 0; i < images.Count; i++)
            {
                Image img = images[i];
                ImageBasicViewModel imgVModel = new ImageBasicViewModel(img);
                viewModel.Images.Add(imgVModel);
            }

            #endregion --- Excute ---

        }
        //end Mapping

        /// <summary>
        /// Void Function: Gọi Service để lấy dữ liệu từ sever => Entity
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productDetails"></param>
        /// <param name="images"></param>
        /// <param name="viewModel"> ViewModel chứa Input</param>
        /// <history>
        /// [Lương Mỹ] Create [29/04/2019]
        /// </history>
        public void GetDataFromSever(int productID, out Product product, out List<ProductDetail> productDetails,
            out List<Image> images, out bool isError, out string message)
        {
            // Call Services
            #region --- Get Data ---
            //Init
            message = string.Empty;
            isError = false;
            product = new Product();
            productDetails = new List<ProductDetail>();
            images = new List<Image>();
            //

            // Check Data in here
            int ProductID = productID;
            // end Check Data

            product = (productID == 0) ? new Product() : productService.Get(ProductID);
            if (product == null)
            {
                message = "Không tìm thấy sản phẩm!";
                // Lỗi
                isError = true;
                return;
            }
            productDetails = detailService.GetList(ProductID);
            foreach(var detail in productDetails)
            {
                int id = detail.Price.GetValueOrDefault();
                var price = priceService.GetOne(id);
            }
            images = imageService.GetList(ProductID);

            #endregion --- Get Data ---

        }
        //end GetDataFromSevice

        #endregion --- Method ---

    }

}