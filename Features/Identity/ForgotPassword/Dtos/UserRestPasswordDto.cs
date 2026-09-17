namespace exam_system.Features.Identity.ForgotPassword.Dtos
{
    public record UserResetPasswordDto(Guid Id, string Email, string FullName);
}
