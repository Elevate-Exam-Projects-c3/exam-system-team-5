using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using MediatR;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetDiplomaAvgScoreQuery(AnalyticsFilterDto Filter) 
        : BaseAnalyticsQuery<IReadOnlyList<DiplomaAvgScoreDto>>(Filter)
    {
        protected override string FeatureName => "avg_score";
    }
}
