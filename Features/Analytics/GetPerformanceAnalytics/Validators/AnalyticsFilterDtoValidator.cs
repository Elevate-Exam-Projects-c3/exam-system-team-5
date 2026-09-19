using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using FluentValidation;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Validators
{
    public class AnalyticsFilterDtoValidator: AbstractValidator<AnalyticsFilterDto>
    {
        public AnalyticsFilterDtoValidator()
        {
            RuleFor(x => x.DateTo)
            .GreaterThanOrEqualTo(x => x.DateFrom)
            .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
            .WithMessage("DateTo must be greater than or equal to DateFrom.");

            RuleFor(x => x.DateFrom)
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .When(x => x.DateFrom.HasValue)
            .WithMessage("DateFrom cannot be in the future.");

            RuleFor(x => x.DiplomaId)
            .NotEmpty()
            .When(x => x.DiplomaId.HasValue)
            .WithMessage("DiplomaId cannot be an empty GUID.");
        }
    }
}
