using System.ComponentModel.DataAnnotations;

namespace MailingService.Models;

public class EmailCredentialsRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Host { get; set; }

    [Required]
    public int Port { get; set; }
}