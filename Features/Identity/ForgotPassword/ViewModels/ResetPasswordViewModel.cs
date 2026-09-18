namespace exam_system.Features.Identity.ForgotPassword.ViewModels
{
    public record ResetPasswordViewModel(string Email, string Otp, string NewPassword);
}
