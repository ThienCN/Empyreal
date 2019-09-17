using Empyreal.Models;
using System.Collections.Generic;

namespace Empyreal.ViewModels.Display
{
    public class DistrictViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int ProvinceId { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        public Province Province { get; set; }
        public List<Ward> Wards { get; set; }

        public DistrictViewModel(District district)
        {
            this.Id = district.Id;
            this.Name = district.Name;
            this.Type = district.Type;
            this.LatiLongTude = district.LatiLongTude;
            this.ProvinceId = district.ProvinceId;
            this.SortOrder = district.SortOrder;
            this.IsPublished = district.IsPublished;
            this.IsDeleted = district.IsDeleted;

            this.Province = district.Province;
            this.Wards = new List<Ward>();
        }
    }
}
