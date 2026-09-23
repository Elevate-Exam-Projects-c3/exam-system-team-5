namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record RecentAttemptSummaryDto(
        Guid AttemptId,
        Guid QuizId,
        string QuizTitle,
        string Status,
        double? Score,
        bool? Passed,
        DateTime StartTime,
        DateTime? SubmittedAt);
}
