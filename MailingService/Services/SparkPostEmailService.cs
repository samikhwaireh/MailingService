using MailingService.Contracts;
using MailingService.Models;
using SparkPost;

namespace MailingService.Services;

public class SparkPostEmailService : IEmailService
{
    public string GetServiceName()
    {
        return "sparkpost";
    }

    public async Task<bool> SendEmailAsync(Email email, string key)
    {
        var client = new Client(key);

        var transmission = new Transmission
        {
            Recipients = email.To.Select(r => new SparkPost.Recipient { Address = new Address { Email = r.Email, Name = r.Name } }).ToList(),
            Content = new SparkPost.Content
            {
                From = new Address { Email = email.From.Email, Name = email.From.Name },
                Subject = email.Subject,
                Html = email.HtmlBody,
                Text = email.TextBody
            }
        };

        try
        {
            var response = await client.Transmissions.Send(transmission);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return false;
        }
    }
}