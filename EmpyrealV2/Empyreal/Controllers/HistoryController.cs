using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels.Display;
using Empyreal.ViewModels.History;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Empyreal.Controllers
{
    public class HistoryController : Controller
    {
        #region --- Variables ---
        // Service
        private readonly ICatalogService catalogService;
        private readonly IProductService productService;
        private readonly IProductDetailService detailService;
        private readonly IProductPriceService priceService;
        private readonly IHistoryService historyService;

        private readonly IImageService imageService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        // Environment
        private IHostingEnvironment _env;

        // Constant 
        private const int MODE_MANAGER = 0;
        private const int MODE_UPDATE = 1;
        private const string TABLE_PRODUCT = "Product";
        #endregion --- Variables ---

        #region --- Constructor ---

        public HistoryController(IHostingEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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
        }

        #endregion --- Constructor ---

        #region --- Request ---

        /// <summary>
        /// Tiếp nhận Request & Lấy dữ liệu lịch sử
        /// </summary>
        /// <param name="detail"> ID of Table.Row</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetHistoryPartial(string detail, int page = 1, int pageSize = 5)
        {
            List<History> historyModel = new List<History>();
            int productID = 0;
            if (String.IsNullOrEmpty(detail) || String.IsNullOrWhiteSpace(detail))
            { // Show ở danh mục
                historyModel = historyService.GetByTable(TABLE_PRODUCT);
            }
            else
            { // Show ở chỉnh sửa
                productID = Int32.Parse(detail);
                historyModel = historyService.GetByDetail(TABLE_PRODUCT, productID);
            }
            int count = historyModel.Count;
            List<HistoryBasicViewModel> viewModel = new List<HistoryBasicViewModel>();
            for (int index = 0; index < count; index++)
            {
                History model = historyModel[index];
                HistoryBasicViewModel tempVModel = new HistoryBasicViewModel()
                {
                    Content = model.Content,
                    Summary = model.Summary,
                    UserID = model.CreateByUser,
                    UserName = model.CreateByUserNavigation.HoTen,
                    CreateDate = model.CreateDate.GetValueOrDefault(),
                    DetailID = model.Detail.ToString()
                };
                viewModel.Add(tempVModel);
                //

            }

            PagedHistoryViewModel paged = new PagedHistoryViewModel(viewModel, page, pageSize);
            paged.DetailID = productID;
            return PartialView("_ShowHistoryPartial", paged);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HistoryManager(string detail, int page = 1, int pageSize = 5)
        {
            //
            List<History> historyModel = new List<History>();
            if (String.IsNullOrEmpty(detail) || String.IsNullOrWhiteSpace(detail))
            { // Show ở danh mục
                historyModel = historyService.GetByTable(TABLE_PRODUCT);
            }
            else
            { // Show ở chỉnh sửa
                int productID = Int32.Parse(detail);
                historyModel = historyService.GetByDetail(TABLE_PRODUCT, productID);
            }
            int count = historyModel.Count;
            List<HistoryBasicViewModel> viewModel = new List<HistoryBasicViewModel>();
            for (int index = 0; index < count; index++)
            {
                History model = historyModel[index];
                HistoryBasicViewModel tempVModel = new HistoryBasicViewModel()
                {
                    Content = model.Content,
                    Summary = model.Summary,
                    UserID = model.CreateByUser,
                    UserName = model.CreateByUserNavigation.HoTen,
                    CreateDate = model.CreateDate.GetValueOrDefault(),
                };
                viewModel.Add(tempVModel);
                //

            }

            PagedHistoryViewModel paged = new PagedHistoryViewModel(viewModel, page, pageSize);

            return PartialView("_ShowHistoryPartial", paged);
        }
        #endregion --- Request ---

    }
}