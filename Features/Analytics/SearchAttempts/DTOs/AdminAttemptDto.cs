using exam_system.Common.Enums;

namespace exam_system.Features.Analytics.SearchAttempts.DTOs
{
    public record AdminAttemptDto(
        Guid AttemptId,
        Guid QuizId,
        string QuizTitle,
        Guid StudentId,
        string? StudentCode,
        string StudentEmail,
        string StudentName,
        AttemptStatus Status,
        DateTime StartTime,
        DateTime? SubmittedAt,
        DateTime Deadline,
        double? Score,
        bool? Passed);
}
