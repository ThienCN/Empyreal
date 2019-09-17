using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Empyreal.Controllers
{
    public class ComboBoxController : Controller
    {
        private const string MODE_MANAGER = "Manager";
        private const string MODE_UPDATE = "Update";

        [HttpGet]
        public IActionResult CreateComboBox(string TypeName)
        {
            IProductTypeService productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            var Types = productTypeService.GetProductType(TypeName, 1);
            //
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(var type in Types)
            {
                items.Add(new SelectListItem { Value= type.Id.ToString(), Text =type.Text});
            }
            //ViewModel
            ComboBoxViewModel cbbViewModel = new ComboBoxViewModel();
            //
            cbbViewModel.Items = items;
            cbbViewModel.Name = TypeName;
            // Set tilte
            string tilte = string.Empty;
            if (TypeName == "Size") tilte = "Kích cỡ";
            else if (TypeName == "Color") tilte = "Màu sắc";
            cbbViewModel.Tilte = tilte;
            //

            return PartialView("_ComboBox", cbbViewModel);
        }

        /// <summary>
        /// Function: Lấy Danh sách Danh mục
        /// </summary>
        /// <history>
        /// [Lương Mỹ] ProductManagerViewModel [20/04/2019]
        /// </history>
        public List<SelectListItem> GetCatalogs(int mode)
        {
            ICatalogService catalogService = ServiceLocator.Current.GetInstance<ICatalogService>();

            //
            List<Catalog> dataCatalog = catalogService.GetAll(1);
            //
            List<SelectListItem> catalogs = new List<SelectListItem>();
            if (mode == 0)
            {
                catalogs.Add(new SelectListItem() { Text = "Tất cả", Value = "0" });
            }
            foreach (var item in dataCatalog)
            {
                catalogs.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return catalogs;
        }

        /// <summary>
        /// Function: Lấy ProductType
        /// </summary>
        /// <param name="TypeName"> Color || Size </param>
        /// <history>
        /// [Lương Mỹ] Create [20/04/2019]
        /// </history>
        public List<SelectListItem> GetProductType(string TypeName)
        {
            IProductTypeService productTypeService = ServiceLocator.Current.GetInstance<IProductTypeService>();
            var Types = productTypeService.GetProductType(TypeName, 1);
            //
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var type in Types)
            {
                items.Add(new SelectListItem { Value = type.Id.ToString(), Text = type.Text });
            }

            return items;
        }
    }
}