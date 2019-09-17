using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Empyreal.Services.Services;
using Empyreal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Empyreal.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            //ICatalogService catalogService = ServiceLocator.Current.GetInstance<ICatalogService>();
            //List<Catalog> enitites = catalogService.AllCatalog();
            //List<CatalogViewModel> catalogs = new List<CatalogViewModel>();
            //foreach ( )
            return View();
        }

    }
}