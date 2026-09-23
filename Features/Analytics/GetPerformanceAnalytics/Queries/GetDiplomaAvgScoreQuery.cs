using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetDiplomaAvgScoreQuery(
        DateTime? DateTime ,
        DateTime? DateTo,
        Guid? DiplomaId) : BaseAnalyticsQuery<IReadOnlyList<DiplomaAvgScoreDto>>(DateTime, DateTo, DiplomaId)
    {
        protected override string FeatureName => "diploma_avg";
    }
}
