using exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Validators
{
    public class UpdateQuestionOrchestratorValidator
        : AbstractValidator<UpdateQuestionOrchestrator>
    {
        public UpdateQuestionOrchestratorValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Question text is required.");

            RuleFor(x => x.Options)
                .NotNull()
                .Must(options => options.Count >= 2)
                .WithMessage("Question must have at least 2 options.");

            RuleFor(x => x.Options)
                .Must(options => options.Count(x => x.IsCorrect) == 1)
                .WithMessage("Question must have exactly one correct option.");
        }
    }
}
