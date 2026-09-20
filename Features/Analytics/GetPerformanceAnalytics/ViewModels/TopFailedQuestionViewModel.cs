namespace exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels
{
    public record TopFailedQuestionViewModel(
    Guid QuestionId,
    string QuestionText,
    string QuizTitle,
    int TotalAnswers,
    int IncorrectAnswers,
    double FailureRatePercentage);
}
