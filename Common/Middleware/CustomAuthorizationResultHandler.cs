using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;
using System.Text.Json;

namespace exam_system.Common.Middleware
{
    public class CustomAuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();
        private static readonly JsonSerializerOptions JsonOption = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                var response = ApiResponse.Fail(
                    "Forbbidden : you do not have premission to access this Response ",
                    (int)HttpStatusCode.Forbidden);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOption));
                return;
            }
            if (authorizeResult.Challenged)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                var response = ApiResponse.Fail(
                    "Unauthorized: Authentication is required",
                    (int)HttpStatusCode.Unauthorized);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOption));
                return;

            }
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
