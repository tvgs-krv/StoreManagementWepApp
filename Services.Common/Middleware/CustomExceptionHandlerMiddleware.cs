using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;

namespace Services.Common.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;


        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<object>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                var methodName = context.Request.Path.Value?.Substring(1);
                if (!string.IsNullOrWhiteSpace(methodName))
                    _logger.LogInformation($"Ответ от {methodName}: StatusCode: 200 (Ok)");
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case NotValidException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case ConflictException:
                    code = HttpStatusCode.Conflict;
                    break;
                case NotCreatedException:
                    code = HttpStatusCode.ServiceUnavailable;
                    break;
                case NotSendException:
                    code = HttpStatusCode.ServiceUnavailable;
                    break;
                case not null:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            
            var request = context.Request;
            request.EnableBuffering();
            request.Body.Position = 0;
            StreamReader stream = new StreamReader(context.Request.Body);
            string body = stream.ReadToEndAsync().GetAwaiter().GetResult();

            _logger.LogError($"Incorrect request. Error: {exception.Message}; " +
                            $"URL: {context.Request.Path.Value?.Substring(1)}; " +
                            $"Method: {request.Method}; " +
                            $"Headers: {string.Join(",", request.Headers.Select(p => $"{p.Key}:{p.Value}"))}, " +
                            $"ContentType: {request.ContentType}, " +
                            $"Query: {request.QueryString.Value}; " +
                            $"DiagnosticActivityId: {System.Diagnostics.Activity.Current?.Id}; " +
                            $"Body: {body}");

            return context.Response.WriteAsync(result);
        }
    }
}