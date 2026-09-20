using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetQuizPassRateQuery(
        DateTime? DateFrom, 
        DateTime? DateTo,
        Guid? DiplomaId)
        : BaseAnalyticsQuery<IReadOnlyList<QuizPassRateDto>>(DateFrom,DateTo,DiplomaId)
    {
        protected override string FeatureName => "pass_rate";
    }
}