namespace MailingService.Models;

public class RegisteredEmail
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecretKey { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    private RegisteredEmail()
    {

    }
    public static RegisteredEmail Create(string email, string password, string secretKey, string host, int port)
    {
        return new RegisteredEmail
        {
            Email = email,
            Password = password,
            SecretKey = secretKey,
            SmtpHost = host,
            SmtpPort = port
        };
    }
}
