using exam_system.Features.Shared;
using FluentValidation;
using System.Text.Json;

namespace exam_system.Common.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;

            IDictionary<string, string[]>? errors = null;

            switch (ex)
            {
                // ==========================================
                // Common / Shared Exceptions
                // ==========================================

                case UnauthorizedAccessException:

                    statusCode = StatusCodes.Status401Unauthorized;

                    message = "You are not authorized to access this resource.";

                    _logger.LogWarning(
                        ex,
                        "Unauthorized access.");

                    break;


                case KeyNotFoundException:

                    statusCode = StatusCodes.Status404NotFound;

                    message = "The requested resource was not found.";

                    _logger.LogWarning(
                        ex,
                        "Resource not found.");

                    break;


                case NotFoundException:

                    statusCode = StatusCodes.Status404NotFound;

                    message = ex.Message;

                    _logger.LogWarning(
                        ex,
                        "Resource not found.");

                    break;


                case ConflictException:

                    statusCode = StatusCodes.Status409Conflict;

                    message = ex.Message;

                    _logger.LogWarning(
                        ex,
                        "Conflict occurred.");

                    break;


                case GoneException:

                    statusCode = StatusCodes.Status410Gone;

                    message = ex.Message;

                    _logger.LogWarning(
                        ex,
                        "Resource is no longer available.");

                    break;




                case ValidationException validationException:

                    statusCode = StatusCodes.Status400BadRequest;

                    message = "Validation failed.";

                    errors = validationException.Errors
                        .GroupBy(error => error.PropertyName)
                        .ToDictionary(
                            group => group.Key,
                            group => group
                                .Select(error => error.ErrorMessage)
                                .ToArray()
                        );

                    _logger.LogWarning(
                        validationException,
                        "Validation failed.");

                    break;




                default:

                    statusCode = StatusCodes.Status500InternalServerError;

                    message = "An unexpected error occurred.";

                    _logger.LogError(
                        ex,
                        "An unhandled exception occurred.");

                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = RequestResponse<object>.Fail(
                message,
                statusCode,
                errors);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(
                response,
                options);

            await context.Response.WriteAsync(json);
        }
    }
}