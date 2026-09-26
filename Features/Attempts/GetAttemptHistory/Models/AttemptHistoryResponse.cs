using exam_system.Common.Enums;

namespace exam_system.Features.Attempts.GetAttemptHistory.Models
{
    public record AttemptHistoryResponse(
     Guid AttemptId,
     Guid QuizId,
     string QuizTitle,
     AttemptStatus Status,
     double? Score,
     DateTime StartTime,
     DateTime? SubmittedAt);

}
