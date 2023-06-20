using MailingService.Contracts;
using MailingService.Models;
using System.Net;
using System.Net.Mail;

namespace MailingService.Services;

public class SmtpEmailService : IEmailService
{
    private readonly IEmailRegistrationService emailRegistrationService;

    public SmtpEmailService(IEmailRegistrationService emailRegistrationService)
    {
        this.emailRegistrationService = emailRegistrationService;
    }

    public string GetServiceName()
    {
        return "smtp";
    }

    public async Task<bool> SendEmailAsync(Email email, string key)
    {
        var registeredEmail = emailRegistrationService.GetRegisteredEmail(key);

        if (registeredEmail == null)
        {
            return false; // Invalid secret key
        }

        try
        {
            using var client = new SmtpClient();

            client.Host = registeredEmail.SmtpHost;
            client.Port = registeredEmail.SmtpPort;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(registeredEmail.Email, registeredEmail.Password);

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From.Email, email.From.Name),
                Subject = email.Subject,
                Body = email.HtmlBody,
                IsBodyHtml = true
            };

            foreach (var recipient in email.To)
            {
                mailMessage.To.Add(new MailAddress(recipient.Email, recipient.Name));
            }

            foreach (var recipient in email.CC)
            {
                mailMessage.CC.Add(new MailAddress(recipient.Email, recipient.Name));
            }

            foreach (var recipient in email.BCC)
            {
                mailMessage.Bcc.Add(new MailAddress(recipient.Email, recipient.Name));
            }

            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
