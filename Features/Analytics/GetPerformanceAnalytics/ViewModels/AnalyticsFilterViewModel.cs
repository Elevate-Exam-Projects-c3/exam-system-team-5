using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels
{
    public record AnalyticsFilterViewModel
    {
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
        public Guid? DiplomaId { get; init; }
        public AnalyticsFilterDto ToDto() => new(DateFrom, DateTo, DiplomaId);
    }
}
