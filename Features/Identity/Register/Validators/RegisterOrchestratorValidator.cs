using exam_system.Features.Identity.Register.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.Register.Validators
{
    public class RegisterOrchestratorValidator : AbstractValidator<RegisterOrchestrator>
    {
        public RegisterOrchestratorValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .Length(2, 100).WithMessage("Full name must be between 2 and 100 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
