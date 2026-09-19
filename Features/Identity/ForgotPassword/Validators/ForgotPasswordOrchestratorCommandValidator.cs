using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.ForgotPassword.Validators
{
    public class ForgotPasswordOrchestratorCommandValidator : AbstractValidator<ForgotPasswordOrchestratorCommand>
    {
        public ForgotPasswordOrchestratorCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");
        }
    }
}
