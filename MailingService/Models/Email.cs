using System.ComponentModel.DataAnnotations;

namespace MailingService.Models;

public class Email
{
    [Required]
    public Sender From { get; set; }
    [Required]
    public List<Recipient> To { get; set; }
    public List<Recipient> CC { get; set; }
    public List<Recipient> BCC { get; set; }
    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string HtmlBody { get; set; }
}

public class Sender
{
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
}

public class Recipient
{
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
}
