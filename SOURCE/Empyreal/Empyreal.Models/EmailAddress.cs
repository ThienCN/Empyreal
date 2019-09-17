using System.ComponentModel.DataAnnotations.Schema;

namespace Empyreal.Models
{
    [NotMapped]
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
