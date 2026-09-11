using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.VerifyEmailOtp.Validators
{
    public class VerifyEmailOrchestratorCommandValidator : AbstractValidator<VerifyEmailOrchestratorCommand>
    {
        public VerifyEmailOrchestratorCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(x=>x.OtpCode)
                .NotEmpty().WithMessage("OTP code is required.")
                .Length(6).WithMessage("OTP code must be 6 characters long.");
        }
    }
}
