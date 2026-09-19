namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs
{
    public record QuizPassRateDto(
            Guid QuizId,
            string QuizTitle,
            int TotalAttempts,
            int PassedAttempts,
            double PassRatePercentage);
}
