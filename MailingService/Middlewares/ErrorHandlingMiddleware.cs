using SendGrid.Helpers.Errors.Model;
using System.Net;
using System.Text.Json;

namespace MailingService.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {

        var statusCode = HttpStatusCode.InternalServerError;
        if (ex is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
        }
        else if (ex is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
        }

        // Set the response content
        var response = new { error = "an error occured!" };
        var jsonResponse = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(jsonResponse);
    }
}
