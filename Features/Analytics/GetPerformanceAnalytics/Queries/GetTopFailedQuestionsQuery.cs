using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetTopFailedQuestionsQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    Guid? DiplomaId,
    int Take = 10,
    int MinAnswers = 1,
    double MaxSuccessRate = 40.0) : BaseAnalyticsQuery<IReadOnlyList<TopFailedQuestionDto>>(DateFrom, DateTo, DiplomaId)
    {
        protected override string FeatureName => $"top_failed_take{Take}_min{MinAnswers}_max{MaxSuccessRate}";
    }
}
