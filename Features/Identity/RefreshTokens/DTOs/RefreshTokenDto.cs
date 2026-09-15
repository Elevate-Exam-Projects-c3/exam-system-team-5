namespace exam_system.Features.Identity.RefreshTokens.DTOs
{
    public record RefreshTokenDto(
        Guid Id,
        Guid UserId,
        string Token,
        DateTime ExpiresAt,
        bool IsUsed,
        bool IsRevoked);

}
