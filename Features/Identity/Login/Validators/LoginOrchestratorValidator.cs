using exam_system.Features.Identity.Login.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.Login.Validators
{
    public class LoginOrchestratorValidator : AbstractValidator<LoginOrchestrator>
    {
        public LoginOrchestratorValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
