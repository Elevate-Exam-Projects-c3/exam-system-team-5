using exam_system.Features.Shared;
using System.Net;
using System.Text.Json;

namespace exam_system.Common.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";

            switch (ex)
            {
                case UnauthorizedAccessException: 
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "You are not authorized to access this resource.";
                    break;

                case KeyNotFoundException: 
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    break;
                default: 
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(ex, "An unhandled exception occurred.");
                    break;
            }

            context.Response.StatusCode = statusCode;

            var responseModel = ApiResponse.Fail(message, statusCode);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(responseModel, options);

            await context.Response.WriteAsync(json);
        }
    }
}