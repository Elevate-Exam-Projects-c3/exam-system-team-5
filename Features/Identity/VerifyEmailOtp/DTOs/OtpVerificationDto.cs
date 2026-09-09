using exam_system.Common.Enums;

namespace exam_system.Features.Identity.VerifyEmailOtp.DTOs
{
    public record OtpVerificationDto(Guid OtpId ,
        Guid UserId ,
        string OtpHash,
        DateTime ExpiresAt,
        int AttemptCount ,
        bool IsUsed,
        AccountStatus AccountStatus);
}
