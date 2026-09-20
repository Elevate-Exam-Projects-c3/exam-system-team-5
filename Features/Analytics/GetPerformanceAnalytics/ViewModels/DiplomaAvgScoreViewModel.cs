namespace exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels
{
    public record DiplomaAvgScoreViewModel
    (
    Guid DiplomaId,
    string DiplomaTitle,
    int TotalAttempts,
    double AverageScore);
}
