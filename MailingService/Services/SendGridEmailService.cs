using MailingService.Contracts;
using MailingService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MailingService.Services;

public class SendGridEmailService : IEmailService
{
    public async Task<bool> SendEmailAsync(Email email, string key)
    {
        var client = new SendGridClient(key);
        var msg = CreateSendGridMessage(email);

        try
        {
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private SendGridMessage CreateSendGridMessage(Email email)
    {
        var msg = new SendGridMessage();
        msg.SetFrom(new EmailAddress(email.From.Email, email.From.Name));
        msg.AddTos(ConvertRecipients(email.To));
        msg.AddCcs(ConvertRecipients(email.CC));
        msg.AddBccs(ConvertRecipients(email.BCC));
        msg.SetSubject(email.Subject);
        msg.AddContent(MimeType.Text, email.TextBody);
        msg.AddContent(MimeType.Html, email.HtmlBody);

        return msg;
    }

    private List<EmailAddress> ConvertRecipients(List<Recipient> recipients)
    {
        return recipients.Select(r => new EmailAddress(r.Email, r.Name)).ToList();
    }

    public string GetServiceName()
    {
        return "sendgrid";
    }
}