using exam_system.Common.Enums;

namespace exam_system.Features.Identity.RefreshTokens.DTOs
{
    public record UserAuthDto(
        Guid Id,
        string Email,
        UserRole Role,
        AccountStatus IsActive);
}
