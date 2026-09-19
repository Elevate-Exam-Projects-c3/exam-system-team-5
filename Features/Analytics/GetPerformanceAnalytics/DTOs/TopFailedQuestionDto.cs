using System.Security.Cryptography;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs
{
    public record TopFailedQuestionDto(
        Guid QuestionId,
        string QuestionText,
        string QuizTitle,
        int TotalAnswers,
        int CorrectAnswers,
        double SuccessRatePercentage);
    
}
