using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Empyreal.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Empyreal.Controllers.Manager
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ProductController : Controller
    {
        #region --- Variables ---
        // Service
        private readonly ICatalogService catalogService;
        private readonly IProductService productService;
        private readonly IProductDetailService detailService;
        private readonly IProductPriceService priceService;
        private readonly IHistoryService historyService;
        private readonly IProductTypeService productTypeService;

        private readonly IImageService imageService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        // Environment
        private IHostingEnvironment _env;

        // Constant 
        private const int MODE_MANAGER = 0;
        private const int MODE_UPDATE = 1;
        private const string TABLE_NAME = "Product";
        #endregion --- Variables ---

        #region --- Constructor ---

        public ProductController(IHostingEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _env = env;

            this.userManager = userManager;
            this.roleManager = roleManager;

            catalogService = ServiceLocator.Current.GetInstance<ICatalogService>();
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            detailService = ServiceLocator.Current.GetInstance<IProductDetailService>();
            priceService = ServiceLocator.Current.GetInstance<IProductPriceService>();
            imageService = ServiceLocator.Current.GetInstance<IImageService>();
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            historyService = ServiceLocator.Current.GetInstance<IHistoryService>();
            productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
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
                keySearch = " ";

            List<Product> products = new List<Product>();

            products = productService.SearchFullText(keySearch, catalogSelect, 1);

            //
            List<ProductBasicViewModel> listProduct = new List<ProductBasicViewModel>();
            foreach (var p in products)
            {
                string userName = string.Empty;
                User user = userService.Get(p.CreateByUser);
                if (user != null)
                    userName = user.HoTen;

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
        public async Task<IActionResult> ProductUpdate(int productID, bool isUpdate)
        {

            ProductUpdateViewModel viewModel = new ProductUpdateViewModel(new Product());
            viewModel.IsUpdate = isUpdate;
            //viewModel.ProductType = JsonConvert.SerializeObject(productTypeService.GetAvaliable());

            #region --- Default --- 


            if (viewModel.IsUpdate)
                ViewData["Title"] = "Chỉnh sửa sản phẩm";
            else
                ViewData["Title"] = "Thêm mới sản phẩm";

            #endregion --- Default --- 

            string message = string.Empty;
            bool isError = false;
            Product product = new Product();
            List<ProductDetail> productDetails = new List<ProductDetail>();
            List<Image> images = new List<Image>();
            //
            bool result = GetDataFromSever(productID, out product, out productDetails, out images, out isError, out message);

            if (result)
            {
                viewModel.IsError = isError;
                viewModel.Message = message;

                //ModelState.AddModelError("IsError", message);
                goto Finish;
            }
            //
            Mapping(product, productDetails, images, ref viewModel);
            //
            InitComboBox(ref viewModel);
            // Use for History
            if (isUpdate)
            {
                string JSON_ProductOld = ParseJson_ProductOld(product);
                viewModel.ProductOld = JSON_ProductOld;

            }

            var ENVIRONMENT_USER_ID = await userManager.GetUserAsync(User);
            if (ENVIRONMENT_USER_ID == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            viewModel.UserID = ENVIRONMENT_USER_ID.Id;
            viewModel.UserName = ENVIRONMENT_USER_ID.UserName;
        //
        Finish:
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
            History history = null;
            //Excute
            try
            {
                DataTranfer(out product, out history, viewModel);
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
                if (!viewModel.IsUpdate)
                {
                    history.Summary = "Thêm mới";
                    history.Content = "Tên sản phẩm: " + product.Name;
                }
                else
                {
                    history.Summary = "Chỉnh sửa";
                }

                isReturn = productService.Update(product, history);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productID, string keySearch, int selectCatalog)
        {
            int isReturn = 0;
            string message = String.Empty;
            // Call Service => Execute
            #region --- Main Execute ---


            // Execute Insert
            try
            {
                Product productDel = productService.Get(productID);
                if (productDel == null)
                {
                    message = "Sản phẩm đã được xóa trước đó!";
                    goto Finish;
                }
                var ENVIRONMENT_USER_ID = await userManager.GetUserAsync(User);
                string UserID = ENVIRONMENT_USER_ID.Id;

                History history = new History();
                history.Table = TABLE_NAME;
                history.Detail = productDel.Id;
                history.CreateByUser = UserID;
                history.Summary = "Xóa";
                history.Content = String.Format("<p> Mã sản phẩm: {0} <br> Tên sản phẩm: {1}.</p>", productDel.Id, productDel.Name);
                history.CreateDate = DateTime.Now;
                isReturn = productService.Delete(productDel, history);

            }
            catch (Exception e)
            {
                message = e.Message;
                goto Finish;
            }


        Finish:

            #endregion --- Main Execute ---
            return RedirectToAction("ProductManager", "Product", new { keySearch = keySearch, catalogSelect = selectCatalog });
        }



        #endregion --- Request --- 

        #region --- Private Method ---

        /// <summary>
        /// Xử lý Input thành ViewModel hoàn chỉnh
        /// </summary>
        /// <param name="viewModel"> ViewModel chứa Input</param>
        /// <history>
        /// [Lương Mỹ] Create [29/04/2019]
        /// </history>
        private void ConfigViewModel(ref ProductUpdateViewModel viewModel)
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
                        return;
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
        private void DataTranfer(out Product product, out History history, ProductUpdateViewModel viewModel)
        {
            bool deleteImage = false;
            bool deleteDetail = false;

            string userID = viewModel.UserID;
            string userName = viewModel.UserName;
            history = new History();

            // ViewModel => Entity
            #region --- Tranfer ---

            #region --- Product ---
            // Product Entity => Product
            int productID = viewModel.ProductId;
            if (productID == 0)
            {
                product = new Product();
                product.CreateByUser = userID;
                product.CreateDate = DateTime.Now;
            }
            else
            {
                product = productService.Get(productID);
                if (product == null) return; // lỗi
            }
            product.Name = viewModel.ProductName;
            //UserID
            product.CatalogId = viewModel.Catalog;
            product.Description = viewModel.Description;
            //History
            product.LastModifyByUser = userID;
            product.LastModifyDate = DateTime.Now;
            //
            product.State = 1;
            #endregion --- Product ---

            #region --- Details + Price ---

            // ProductDetail Entity => ProductDetails
            var productDetails = new List<ProductDetail>();
            for (int i = 0; i < viewModel.ProductDetails.Count; i++)
            {
                var proDetailModel = viewModel.ProductDetails[i];

                int detailID = proDetailModel.ID;
                ProductDetail detail = null;
                if (detailID == 0)
                {   // Thêm mới
                    detail = new ProductDetail();
                    detail.CreateByUser = userID;
                    detail.CreateDate = DateTime.Now;
                }
                else
                { // Chỉnh sửa
                    detail = detailService.GetOne(detailID);
                    if (detail == null) continue;
                }
                // Info
                detail.Id = proDetailModel.ID;
                detail.Size = proDetailModel.Size;
                detail.Color = proDetailModel.Color;
                detail.Quantity = proDetailModel.Quantity;
                detail.Price = proDetailModel.PriceID;
                // History
                detail.LastModifyByUser = userID;
                detail.LastModifyDate = DateTime.Now;
                // State
                detail.State = 1;

                // ProductPrice
                int priceID = proDetailModel.PriceID.GetValueOrDefault();

                ProductPrice productPrice = null;
                var newPrice = Double.Parse(viewModel.PriceText[i].Replace(".", ","));

                if (priceID != 0)
                {
                    productPrice = priceService.GetOne(priceID);
                    //var newPrice = Double.Parse(viewModel.PriceText[i].Replace(".",","));
                    // Tạo mới Price nếu Thay đổi giá tiền
                    if (productPrice.Price != newPrice)
                    {
                        productPrice = new ProductPrice();
                        productPrice.CreateDate = DateTime.Now;
                        productPrice.CreateByUser = userID;
                    }
                }
                else
                {
                    productPrice = new ProductPrice();
                    productPrice.CreateDate = DateTime.Now;
                    productPrice.CreateByUser = userID;
                }
                productPrice.Price = newPrice;
                // History
                productPrice.LastModifyByUser = userID;
                productPrice.LastModifyDate = DateTime.Now;
                //
                productPrice.State = 1;

                detail.PriceNavigation = productPrice;
                productDetails.Add(detail);

            }//end Entity ProductDetail

            // Delete ProductDetail
            if (viewModel.DeleteDetailID != null)
            {
                for (int i = 0; i < viewModel.DeleteDetailID.Count; i++)
                {
                    deleteDetail = true;
                    var proDetailID = viewModel.DeleteDetailID[i];

                    // ProductDetail
                    ProductDetail detail = detailService.GetOne(proDetailID);
                    if (detail == null) continue;

                    detail.State = 0; // Xoa
                                      // Update history
                    detail.LastModifyByUser = userID;
                    detail.LastModifyDate = DateTime.Now;

                    productDetails.Add(detail);

                }
            }

            #endregion --- Details + Price ---

            #region --- Image ---


            // Image Entity => List<Image>
            var images = new List<Image>();
            
            for (int i = 0; i < viewModel.Images.Count; i++)
            {
                var imgModel = viewModel.Images[i];

                Image img = new Image()
                {
                    Url = imgModel.Url,
                    CreateByUser = userID

                };
                images.Add(img);
            }


            // Delete ProductDetail
            if (viewModel.DeleteImageID != null)
            {
                for (int i = 0; i < viewModel.DeleteImageID.Count; i++)
                {
                    var imageDetailID = viewModel.DeleteImageID[i];

                    // ProductDetail
                    Image img = imageService.GetOne(imageDetailID);
                    if (img == null) continue;

                    img.State = 0; // Xoa
                                   // Update history
                    img.LastModifyByUser = userID;
                    img.LastModifyDate = DateTime.Now;

                    images.Add(img);
                }

            }
            #endregion --- Image ---

            #region --- History ---

            history.CreateDate = DateTime.Now;
            history.CreateByUser = userID;
            String changes = String.Empty;

            
            if(!viewModel.IsUpdate) // Thêm mới
            {
                changes = String.Format("</p> Tạo mới sản phảm </p>");
            }
            else
            {
                if(viewModel.History!= null)
                {
                    for (int i = 0; i < viewModel.History.Count; i++)
                    {
                        var item = viewModel.History[i];
                        item = String.Format("<p> {0} </p>", item.ToString());
                        viewModel.History[i] = item;
                    }
                    changes = String.Join(" ", viewModel.History);
                }

                if (viewModel.DeleteImageID != null && viewModel.DeleteImageID.Count > 0)
                {
                    changes += String.Format("</p> Xóa {0} hình ảnh </p>", viewModel.DeleteImageID.Count);
                }
                if (viewModel.Images != null && viewModel.Images.Count > 0)
                {
                    changes += String.Format("</p> Thêm {0} hình ảnh </p>", viewModel.Images.Count);
                }
            }


            history.Content = changes;
            history.Table = "Product";

            #endregion --- History ---

            if (viewModel.IsUpdate)
            {
                foreach (var img in images)
                {
                    product.Images.Add(img);

                }
                foreach (var detail in productDetails)
                {
                    product.ProductDetails.Add(detail);
                }
            }
            else
            {
                product.Images = images;
                product.ProductDetails = productDetails;
            }


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
        private void Mapping(Product product, List<ProductDetail> productDetails, List<Image> images,
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
        /// Gọi Service để lấy dữ liệu từ sever => Entity :
        /// True = Error || False = Success
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productDetails"></param>
        /// <param name="images"></param>
        /// <param name="viewModel"> ViewModel chứa Input</param>
        /// <returns>
        /// True = Error || False = Success
        /// </returns>
        /// <history>
        /// [Lương Mỹ] Create [29/04/2019]
        /// </history>
        private bool GetDataFromSever(int productID, out Product product, out List<ProductDetail> productDetails,
            out List<Image> images, out bool isError, out string message)
        {
            // Call Services
            #region --- Get Data ---
            //Init
            message = string.Empty;
            isError = false;
            //
            product = new Product();
            productDetails = new List<ProductDetail>();
            images = new List<Image>();

            // Check Data in here
            int ProductID = productID;

            // end Check Data

            product = (productID == 0) ? new Product() : productService.Get(ProductID);
            if (product == null)
            {
                message = "Không tìm thấy sản phẩm!";
                // Lỗi
                isError = true;
                return isError;
            }
            productDetails = detailService.GetList(ProductID);
            foreach (var detail in productDetails)
            {
                int id = detail.Price.GetValueOrDefault();
                var price = priceService.GetOne(id);
            }
            images = imageService.GetList(ProductID);

            #endregion --- Get Data ---
            // Success
            return false;
        }
        //end GetDataFromSevice


        /// <summary>
        /// Khởi tạo ComboBox cho Create / Update
        /// </summary>
        /// <returns>
        /// out viewModel
        /// </returns>
        private void InitComboBox(ref ProductUpdateViewModel viewModel)
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
        /// Get Product Old in Sever & Parse to JSon
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private string ParseJson_ProductOld(Product product)
        {
            string json = String.Empty;

            if (product == null)
                return String.Empty;
            ProductUpdateViewModel model = new ProductUpdateViewModel(product);
            model.Description = WebUtility.HtmlDecode(model.Description);
            foreach (var detail in product.ProductDetails)
            {
                ProductDetailBasicViewModel detailModel = new ProductDetailBasicViewModel(detail);
                model.ProductDetails.Add(detailModel);
            }
            json = JsonConvert.SerializeObject(model);
            return json;
        }

        #endregion --- Private Method ---

    }

}