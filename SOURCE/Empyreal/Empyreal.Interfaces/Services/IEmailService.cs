using Empyreal.Models;

namespace Empyreal.Interfaces.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }
}
