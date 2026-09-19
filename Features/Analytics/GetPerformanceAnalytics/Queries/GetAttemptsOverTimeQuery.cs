using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using MediatR;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetAttemptsOverTimeQuery(AnalyticsFilterDto Filter)
        : BaseAnalyticsQuery<IReadOnlyList<AttemptsOverTimeDto>>(Filter)
    {
        protected override string FeatureName => "attempts_time";
    }
}
