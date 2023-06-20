namespace MailingService.Middlewares
{
    public class KeyValidationMiddleware
    {
        private readonly RequestDelegate next;

        public KeyValidationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/email/send"))
            {
                if (!context.Request.Headers.ContainsKey("key"))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Key is missing in the request header");
                    return;
                }
            }

            await next(context);
        }
    }
}
