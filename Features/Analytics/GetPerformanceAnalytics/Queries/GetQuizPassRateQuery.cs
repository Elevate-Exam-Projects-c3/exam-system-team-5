using exam_system.Features.Analytics.Common;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using MediatR;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetQuizPassRateQuery(AnalyticsFilterDto Filter) : BaseAnalyticsQuery<IReadOnlyList<QuizPassRateDto>>(Filter)
    {
        protected override string FeatureName => "pass_rate";
    }
}
