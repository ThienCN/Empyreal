using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empyreal.Models
{
    [NotMapped]
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
