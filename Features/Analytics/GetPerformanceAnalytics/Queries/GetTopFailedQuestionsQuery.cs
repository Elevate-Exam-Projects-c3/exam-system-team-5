using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using MediatR;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public record GetTopFailedQuestionsQuery(AnalyticsFilterDto Filter, int Take, int MinAnswers = 1, double MaxSuccessRate = 40.0)
        : BaseAnalyticsQuery<IReadOnlyList<TopFailedQuestionDto>>(Filter)
    {
        protected override string FeatureName => $"top_failed_take{Take}_min{MinAnswers}";
    }
}
