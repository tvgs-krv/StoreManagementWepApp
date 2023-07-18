using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Services.Common.Middleware;

public class LoggingInputDataFromControllerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggingInputDataFromControllerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<object>();
    }

    public async Task Invoke(HttpContext context)
    {
        await LogRequest(context);
        await _next(context);
    }

    private async Task LogRequest(HttpContext context)
    {
        string requestContent;
        context.Request.EnableBuffering();
        var body = context.Request.Body;
        using (var reader = new StreamReader(body, Encoding.UTF8, true, 1024, true))
        {
            requestContent = await reader.ReadToEndAsync();
        }
        body.Position = 0;
        var methodName = context.Request.Path.Value?.Substring(1);
        if (!string.IsNullOrWhiteSpace(requestContent))
            _logger.LogInformation($"Запрос на {methodName}: {requestContent}");
    }
}