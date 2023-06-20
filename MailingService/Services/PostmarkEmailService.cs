using MailingService.Contracts;
using MailingService.Models;
using PostmarkDotNet;

namespace MailingService.Services;

public class PostmarkEmailService : IEmailService
{
    public string GetServiceName()
    {
        return "postmark";
    }

    public async Task<bool> SendEmailAsync(Email email, string key)
    {
        var client = new PostmarkClient(key);

        var message = new PostmarkMessage
        {
            From = email.From.Email,
            To = string.Join(",", email.To.Select(r => r.Email)),
            Cc = string.Join(",", email.CC?.Select(r => r.Email)),
            Bcc = string.Join(",", email.BCC?.Select(r => r.Email)),
            Subject = email.Subject,
            HtmlBody = email.HtmlBody,
            TextBody = email.TextBody
        };

        try
        {
            var response = await client.SendMessageAsync(message);
            return response.Status == PostmarkStatus.Success;
        }
        catch (Exception)
        {
            return false;
        }
    }
}