using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.ViewModels.Display
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? TelephoneCode { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        public List<DistrictViewModel> Districts { get; set; }

        public ProvinceViewModel(Province province)
        {
            this.Id = province.Id;
            this.Name = province.Name;
            this.Type = province.Type;
            this.TelephoneCode = province.TelephoneCode;
            this.ZipCode = province.ZipCode;
            this.CountryId = province.CountryId;
            this.CountryCode = province.CountryCode;
            this.SortOrder = province.SortOrder;
            this.IsPublished = province.IsPublished;
            this.IsDeleted = province.IsDeleted;

            this.Districts = new List<DistrictViewModel>();
        }
    }
}
