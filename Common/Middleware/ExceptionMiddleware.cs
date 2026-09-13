using exam_system.Features.Shared;
using FluentValidation;
using System.Text.Json;

namespace exam_system.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
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
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed.");

                var errors = ex.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group
                            .Select(error => error.ErrorMessage)
                            .ToArray()
                    );

                var response = RequestResponse<object>.Fail(
                    "Validation failed.",
                    StatusCodes.Status400BadRequest,
                    errors);

                context.Response.StatusCode =
                    StatusCodes.Status400BadRequest;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found.");

                var response = RequestResponse<object>.Fail(
                    ex.Message,
                    StatusCodes.Status404NotFound);

                context.Response.StatusCode =
                    StatusCodes.Status404NotFound;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "Conflict occurred.");

                var response = RequestResponse<object>.Fail(
                    ex.Message,
                    StatusCodes.Status409Conflict);

                context.Response.StatusCode =
                    StatusCodes.Status409Conflict;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected error occurred.");

                var response = RequestResponse<object>.Fail(
                    "An unexpected error occurred.",
                    StatusCodes.Status500InternalServerError);

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
        }
    }
}
