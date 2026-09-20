namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs
{
    public record TopFailedQuestionDto(
        Guid QuestionId,
        string QuestionText,
        string QuizTitle,
        int TotalAnswers,
        int IncorrectAnswers,
        double FailureRatePercentage);
    
}
