using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using FluentValidation;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Validators
{
    public class GetTopFailedQuestionsQueryValidator : AbstractValidator<GetTopFailedQuestionsQuery>
    {
        public GetTopFailedQuestionsQueryValidator()
        {
            RuleFor(x => x.Filter)
            .NotNull()
            .WithMessage("Filter must be provided.")
            .SetValidator(new AnalyticsFilterDtoValidator()!);

            RuleFor(x => x.Take)
            .InclusiveBetween(1, 100)
            .WithMessage("Take must be between 1 and 100.");

            RuleFor(x => x.MinAnswers)
            .GreaterThanOrEqualTo(1)
            .WithMessage("MinAnswers must be at least 1.");

            RuleFor(x => x.MaxSuccessRate)
            .InclusiveBetween(0.0, 100.0)
            .WithMessage("MaxSuccessRate must be between 0 and 100.");
        }
    }
}
