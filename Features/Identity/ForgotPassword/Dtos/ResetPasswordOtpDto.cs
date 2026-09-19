namespace exam_system.Features.Identity.ForgotPassword.Dtos
{
    //Expiration timestamp for the reset token. Remains null in Step 1
    //and is only set in Step 2 after successful OTP verification.
    public record ResetPasswordOtpDto(Guid Id, Guid UserId, DateTime? ResetTokenExpiresAt);
}
