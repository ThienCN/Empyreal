using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels
{
    public class CatalogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int State { get; set; }
    }
}
