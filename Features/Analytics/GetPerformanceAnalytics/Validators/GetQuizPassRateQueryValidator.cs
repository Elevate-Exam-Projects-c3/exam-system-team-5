using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using FluentValidation;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Validators
{
    public class GetQuizPassRateQueryValidator : AbstractValidator<GetQuizPassRateQuery>
    {
        public GetQuizPassRateQueryValidator()
        {
            RuleFor(x => x.DiplomaId)
            .Must(id => id != Guid.Empty)
            .When(x => x.DiplomaId.HasValue)
            .WithMessage("DiplomaId cannot be an empty GUID.");

            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(x => x.DateFrom!.Value)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("DateTo must be greater than or equal to DateFrom.");
        }
    }
}
