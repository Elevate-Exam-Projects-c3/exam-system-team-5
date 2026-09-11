namespace exam_system.Features.Quizzes.AdminUpdateQuiz.DTOs
{

    public record UpdateQuizRequestDto(
        string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int PassScore = 60,
        int? MaxAttempts = null
    );
}
