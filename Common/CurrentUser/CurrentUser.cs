// Common/CurrentUser/CurrentUser.cs
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace exam_system.Common.CurrentUser
{
    public class CurrentUser : ICurrentUser
    {
            private readonly ClaimsPrincipal? _user;
            public CurrentUser(IHttpContextAccessor httpContextAccessor) => _user = httpContextAccessor.HttpContext?.User;

            public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

            public Guid UserId
            {
                get
                {
                    var value = _user?.FindFirstValue(ClaimTypes.NameIdentifier);
                    return Guid.TryParse(value, out var id) ? id : Guid.Empty;
                }
            }

            public string? Email => _user?.FindFirstValue(ClaimTypes.Email);
            public string? Role => _user?.FindFirstValue(ClaimTypes.Role);
        
    }
}