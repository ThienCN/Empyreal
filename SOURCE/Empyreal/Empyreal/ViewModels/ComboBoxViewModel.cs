using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels
{
    public class ComboBoxViewModel
    {
        public int ProductDetailID { get; set; }
        public string Name { get; set; }
        public string Tilte { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
