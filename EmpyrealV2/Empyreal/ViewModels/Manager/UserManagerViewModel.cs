using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System.Collections.Generic;
using System.Linq;

namespace Empyreal.ViewModels.Manager
{
    public class UserManagerViewModel
    {
        #region --- Variables ---

        public PagedList<UserBasicViewModel> PagedUserModel { get; set; }
        public List<SelectListItem> CbbCatalog { get; set; }
        public string Keyword { get; set; }

        #endregion --- Variables ---

        #region --- Constructor ---

        public UserManagerViewModel(List<UserBasicViewModel> users, int page,
            int pageSize, string keyWord)
        {
            this.PagedUserModel = new PagedList<UserBasicViewModel>(users.AsQueryable(), page, pageSize);
            this.Keyword = keyWord;
        }

        #endregion --- Constructor ---
    }
}
