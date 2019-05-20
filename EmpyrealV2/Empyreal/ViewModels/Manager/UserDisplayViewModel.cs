using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System.Collections.Generic;

namespace Empyreal.ViewModels.Manager
{
    public class UserDisplayViewModel
    {
        public PagedList<UserBasicViewModel> PagedUserModel { get; set; }
        public List<SelectListItem> CbbCatalog { get; set; }
        public int Catalog { get; set; }
        public string Keyword { get; set; }
    }
}
