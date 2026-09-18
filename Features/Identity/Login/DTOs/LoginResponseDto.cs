using exam_system.Common.Enums;

namespace exam_system.Features.Identity.Login.DTOs
{
    public record LoginResponseDto(Guid UserId,
        string FullName,
        string Email,
        UserRole UserRole,
        string AccessToken,
        string RefreshToken,
        DateTime RefreshTokenExpiresAt);
    
}
