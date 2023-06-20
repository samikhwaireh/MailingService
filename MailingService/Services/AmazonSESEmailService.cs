using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using MailingService.Contracts;
using MailingService.Models;
using Amazon;

namespace MailingService.Services;

public class AmazonSESEmailService : IEmailService
{
    private string accessKey = string.Empty;
    private string region = string.Empty;

    public string GetServiceName()
    {
        return "amazon";
    }

    public async Task<bool> SendEmailAsync(Email email, string key)
    {
        using var client = new AmazonSimpleEmailServiceClient(accessKey, key, RegionEndpoint.GetBySystemName(region));

        var sendRequest = new SendEmailRequest
        {
            Source = email.From.Email,
            Destination = new Destination
            {
                ToAddresses = email.To.Select(r => r.Email).ToList(),
                CcAddresses = email.CC?.Select(r => r.Email).ToList(),
                BccAddresses = email.BCC?.Select(r => r.Email).ToList()
            },
            Message = new Message
            {
                Subject = new Content(email.Subject),
                Body = new Body
                {
                    Html = new Content(email.HtmlBody),
                    Text = new Content(email.TextBody)
                }
            }
        };

        try
        {
            var response = await client.SendEmailAsync(sendRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void SetCredentials(string accessKey, string region)
    {
        this.accessKey = accessKey;
        this.region = region;
    }
}
