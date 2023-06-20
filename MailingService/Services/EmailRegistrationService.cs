using MailingService.Contracts;
using MailingService.Models;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace MailingService.Services;

public class EmailRegistrationService : IEmailRegistrationService
{
    private readonly ConcurrentDictionary<string, RegisteredEmail> registeredEmails = new ConcurrentDictionary<string, RegisteredEmail>();

    public string RegisterEmailCredentials(string email, string password, string host, Int32 port)
    {
        var tokenKey = GenerateToken();
        var registeredEmail = RegisteredEmail.Create(email, password, tokenKey, host, port);

        registeredEmails[tokenKey] = registeredEmail;

        return tokenKey;
    }

    public RegisteredEmail GetRegisteredEmail(string secretKey)
    {
        if (!registeredEmails.TryGetValue(secretKey, out var registeredEmail))
        {
            throw new Exception();
        }
        return registeredEmail;
    }

    private string GenerateToken()
    {
        var hmac = new HMACSHA256();
        return Convert.ToBase64String(hmac.Key);
    }
}
