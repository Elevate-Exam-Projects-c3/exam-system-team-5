using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Events
{
    public record PasswordResetOtpCreatedNotification(string Email, string FullName, string PlainOtp):INotification;
}
