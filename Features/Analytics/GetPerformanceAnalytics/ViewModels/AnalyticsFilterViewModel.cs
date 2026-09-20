using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels
{
    public record AnalyticsFilterViewModel(
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? DiplomaId = null
);
}
