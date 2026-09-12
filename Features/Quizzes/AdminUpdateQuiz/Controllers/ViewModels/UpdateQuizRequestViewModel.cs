namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers.ViewModels
{
    public record UpdateQuizRequestViewModel
    (
        string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int? MaxAttempts = null,
        int PassScore = 60
    );
 
}
