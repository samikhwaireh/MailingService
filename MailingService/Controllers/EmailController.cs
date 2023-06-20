using MailingService.Contracts;
using MailingService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MailingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly Dictionary<string, IEmailService> emailServices;
    private readonly IEmailRegistrationService emailRegistrationService;

    public EmailController(IEnumerable<IEmailService> emailServices, IEmailRegistrationService emailRegistrationService)
    {
        // Create a dictionary of email services with the service name as the key
        this.emailServices = emailServices.ToDictionary(service => service.GetServiceName());
        this.emailRegistrationService = emailRegistrationService;
    }

    [HttpPost("send/{service}")]
    public async Task<IActionResult> SendEmail([FromRoute] string service, [FromBody] Email email)
    {
        // Get the email service based on the requested service name
        if (!emailServices.TryGetValue(service, out var emailService))
        {
            return NotFound($"service not supported");
        }

        // Extract the secret key from the request headers
        if (!Request.Headers.TryGetValue("key", out var keyValues) || keyValues.Count == 0)
        {
            return BadRequest(); // Key not provided
        }

        var key = keyValues.First();

        // Send the email using the appropriate service
        var success = await emailService.SendEmailAsync(email, key);

        if (success)
        {
            return Ok();
        }

        return StatusCode(500);
    }

    [HttpPost("register")]
    public IActionResult RegisterEmailCredentials([FromBody] EmailCredentialsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tokenKey = emailRegistrationService.RegisterEmailCredentials(request.Email, request.Password, request.Host, request.Port);

        return Ok(new { TokenKey = tokenKey });
    }
}
