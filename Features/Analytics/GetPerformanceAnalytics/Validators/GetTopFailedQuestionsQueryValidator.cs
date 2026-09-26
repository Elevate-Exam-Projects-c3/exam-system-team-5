using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using FluentValidation;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Validators
{
    public class GetTopFailedQuestionsQueryValidator : AbstractValidator<GetTopFailedQuestionsQuery>
    {
        public GetTopFailedQuestionsQueryValidator()
        {
            RuleFor(x => x.DiplomaId)
                .Must(id => id != Guid.Empty)
                .When(x => x.DiplomaId.HasValue)
                .WithMessage("DiplomaId cannot be an empty GUID.");

            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(x => x.DateFrom!.Value)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("DateTo must be greater than or equal to DateFrom.");

            RuleFor(x => x.Take)
                .InclusiveBetween(1, 100)
                .WithMessage("Take must be between 1 and 100.");

            RuleFor(x => x.MinAnswers)
                .GreaterThanOrEqualTo(1)
                .WithMessage("MinAnswers must be at least 1.");

            RuleFor(x => x.MaxSuccessRate)
                .InclusiveBetween(0.0, 100.0)
                .WithMessage("MaxSuccessRate must be between 0.0 and 100.0.");
        }
    }
}
