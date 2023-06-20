using MailingService.Models;

namespace MailingService.Contracts
{
    public interface IEmailRegistrationService
    {
        RegisteredEmail GetRegisteredEmail(string secretKey);
        string RegisterEmailCredentials(string email, string password, string host, int port);
    }
}