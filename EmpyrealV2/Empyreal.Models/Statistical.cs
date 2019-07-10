using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empyreal.Models
{
    [NotMapped]
    public class Statistical
    {
        [Key]
        public int Month { get; set; }
        public double MonthlyRevenue { get; set; }
        public int NumberOfOrders { get; set; }
        public int MonthlyNewUser { get; set; }
        public int NewUser { get; set; }
        public double Revenue { get; set; }
        public int Orders { get; set; }
    }
}
