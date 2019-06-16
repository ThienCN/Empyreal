using Empyreal.Models;

namespace Empyreal.ViewModels.Display
{
    public class WardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int DistrictId { get; set; }
        public int SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        public District District { get; set; }

        public WardViewModel(Ward ward)
        {
            this.Id = ward.Id;
            this.Name = ward.Name;
            this.Type = ward.Type;
            this.LatiLongTude = ward.LatiLongTude;
            this.DistrictId = ward.DistrictId;
            this.SortOrder = ward.SortOrder;
            this.IsPublished = ward.IsPublished;
            this.IsDeleted = ward.IsDeleted;

            this.District = ward.District;
        }
    }
}
