using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.ForgotPassword.Validators
{
    public class ResetPasswordOrchestratorCommandValidator: AbstractValidator<ResetPasswordOrchestratorCommand>
    {
        public ResetPasswordOrchestratorCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("OTP is required.")
            .Length(6).WithMessage("OTP must be exactly 6 digits.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
