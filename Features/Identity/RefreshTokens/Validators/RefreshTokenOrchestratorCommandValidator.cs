using exam_system.Features.Identity.RefreshTokens.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.RefreshTokens.Validators
{
    public class RefreshTokenOrchestratorCommandValidator : AbstractValidator<RefreshTokenOrchestratorCommand>
    {
        public RefreshTokenOrchestratorCommandValidator()
        => RuleFor(x => x.RefToken)
          .NotEmpty().WithMessage("Refresh token is required.")
          .NotNull().WithMessage("Refresh token cannot be null.");   
    }
}
