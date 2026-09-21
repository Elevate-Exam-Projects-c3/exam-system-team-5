namespace exam_system.Features.Diplomas.GetStudentDashboard.Controllers.ViewModels
{
    public record RecentAttemptSummaryViewModel(
        Guid AttemptId,
        Guid QuizId,
        string QuizTitle,
        string Status,
        double? Score,
        bool? Passed,
        DateTime StartTime,
        DateTime? SubmittedAt);
}
