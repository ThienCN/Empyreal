using System.Collections.Generic;

namespace Empyreal.Models
{
    public partial class District
    {
        public District()
        {
            Ward = new HashSet<Ward>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int ProvinceId { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Ward> Ward { get; set; }
    }
}
