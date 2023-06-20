using MailingService.Models;

namespace MailingService.Contracts;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email, string key);
    public string GetServiceName();
}
