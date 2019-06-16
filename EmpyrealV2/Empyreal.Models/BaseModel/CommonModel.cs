using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empyreal.Models.BaseModel
{
    [NotMapped]
    public class CommonModel
    {
        public class RatePercent
        {
            [Key]
            public int Star { get; set; }
            public double Percent { get; set; }
        }
    }
}
