namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs
{
    public record AnalyticsFilterDto(
        DateTime? DateFrom = null,
        DateTime? DateTo = null,
        Guid? DiplomaId = null);
}
