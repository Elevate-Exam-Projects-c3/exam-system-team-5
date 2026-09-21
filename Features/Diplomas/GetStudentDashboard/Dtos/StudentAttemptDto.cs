using exam_system.Common.Enums;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record StudentAttemptDto(
        Guid Id,
        Guid QuizId,
        string QuizTitle,
        AttemptStatus Status,
        decimal? Score,
        bool? Passed,
        DateTime StartTime,
        DateTime? SubmittedAt);
}
