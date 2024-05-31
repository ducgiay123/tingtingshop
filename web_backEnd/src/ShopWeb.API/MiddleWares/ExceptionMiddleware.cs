using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Core.src.Common;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShopWeb.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                await HandleExceptionAsync(httpContext, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddleware> logger)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                AppException appEx => (int)appEx.StatusCode,
                DbUpdateException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                ApplicationException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.UnprocessableEntity,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception switch
                {
                    AppException appEx => appEx.Message,
                    DbUpdateException dbUpdateEx => dbUpdateEx.InnerException?.Message ?? "A database update error occurred.",
                    ArgumentException argEx => argEx.Message,
                    ApplicationException appEx => appEx.Message,
                    InvalidOperationException opEx => opEx.Message,
                    _ => "An unexpected error occurred."
                }
            };

            var options = new JsonSerializerOptions { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
            var json = JsonSerializer.Serialize(errorDetails, options);

            await context.Response.WriteAsync(json);

            // Logging based on exception type
            switch (exception)
            {
                case AppException appEx:
                    logger.LogError($"Application error: {appEx.Message}", appEx);
                    break;
                case DbUpdateException dbUpdateEx:
                    logger.LogError($"Database update error: {errorDetails.Message}", dbUpdateEx);
                    break;
                case ArgumentException argEx:
                    logger.LogWarning($"Bad request: {argEx.Message}", argEx);
                    break;
                case ApplicationException appEx:
                    logger.LogWarning($"Application error: {appEx.Message}", appEx);
                    break;
                case InvalidOperationException opEx:
                    logger.LogWarning($"Invalid operation: {opEx.Message}", opEx);
                    break;
                default:
                    logger.LogError($"Unhandled exception: {exception.Message}", exception);
                    break;
            }
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
