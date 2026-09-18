namespace exam_system.Features.Identity.ForgotPassword.Dtos
{
    public record LatesOtpDto(
        Guid Id,
        Guid UserId,
        string OtpHash,
        DateTime CreatedAt,
        DateTime ExpiresAt,
        int AttempCount,
        bool IsUsed);

}
