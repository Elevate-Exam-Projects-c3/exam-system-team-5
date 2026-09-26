using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetAttemptsOverTimeQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    Guid? DiplomaId) : BaseAnalyticsQuery<IReadOnlyList<AttemptsOverTimeDto>>(DateFrom, DateTo, DiplomaId)
    {
        protected override string FeatureName => "attempts_over_time";
    }
}
