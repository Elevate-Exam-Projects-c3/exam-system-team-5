namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers.ViewModels
{
    public record UpdateQuizRequestViewModel
    (
        string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int PassScore = 60,
        int? MaxAttempts = null
    );
 
}
