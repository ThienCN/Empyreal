using Empyreal.ViewModels.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels
{
    public class HomeViewModel
    {
        public ProductBasicViewModel HotProductModels { get; set; }
        public List<ProductBasicViewModel> TopProducts { get; set; }
        public List<ProductBasicViewModel> YourProducts { get; set; }
        public List<ProductBasicViewModel> NewProdcuts { get; set; }
        public string KeySearch { get; set; }
    }
}
