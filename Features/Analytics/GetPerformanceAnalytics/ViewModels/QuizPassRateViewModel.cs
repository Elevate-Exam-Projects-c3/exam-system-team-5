namespace exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels
{
    public record QuizPassRateViewModel
    (
      Guid QuizId,
      string QuizTitle,
      int TotalAttempts,
      int PassedAttempts,
      double PassRatePercentage);
}
