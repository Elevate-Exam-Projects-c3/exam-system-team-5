namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs
{
    public record DiplomaAvgScoreDto(
        Guid DiplomaId,
        string DiplomaTitle,
        double AverageScore,
        int TotalAttempts);
}
