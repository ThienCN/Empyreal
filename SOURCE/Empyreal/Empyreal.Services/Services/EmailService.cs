using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using System.Net;
using System.Net.Mail;

namespace Empyreal.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void Send(EmailMessage emailMessage)
        {
            //var message = new MimeMessage();
            //// To address
            //message.To.AddRange(emailMessage.ToAddresses.Select(m => new MailboxAddress(m.Name, m.Address)));
            //// From address
            //message.From.Add(new MailboxAddress("Empyreal", "empyrealsaleswebsite@gmail.com"));
            //// Mail subject
            //message.Subject = emailMessage.Subject;
            //// Mail body
            //message.Body = new TextPart(TextFormat.Html)
            //{
            //    Text = emailMessage.Content
            //};

            // Configure and send email
            SmtpClient client = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

            MailMessage message = new MailMessage();
            message.From = new MailAddress("empyrealsaleswebsite@gmail.com");
            message.To.Add(emailMessage.ToAddress);
            message.Subject = emailMessage.Subject;
            message.IsBodyHtml = true;
            message.Body = emailMessage.Content;

            client.Send(message);
        }
    }
}
