using AspNetCoreRateLimit;
using MailingService.Contracts;
using MailingService.Filters;
using MailingService.Middlewares;
using MailingService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add rate limiting services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


// Add services to the container.
builder.Services.AddSingleton<IEmailRegistrationService, EmailRegistrationService>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<IEmailService, SendGridEmailService>();
builder.Services.AddScoped<IEmailService, AmazonSESEmailService>();
builder.Services.AddScoped<IEmailService, SparkPostEmailService>();
builder.Services.AddScoped<IEmailService, PostmarkEmailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "lightweight Email Wrapper API", Version = "v1" });

    c.OperationFilter<AddKeyHeaderParameter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

// Enable rate limiting middleware
//app.UseIpRateLimiting();

// Enable Error Handler
app.UseMiddleware<ErrorHandlingMiddleware>();

// Enable key validation middleware
app.UseMiddleware<KeyValidationMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
