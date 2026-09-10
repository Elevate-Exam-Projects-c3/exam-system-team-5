namespace exam_system.Features.Quizzes.AdminCreateQuiz.Controllers.ViewModels
{
    public record CreateQuizRequestViewModel(
                string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int PassScore = 60,
        int? MaxAttempts = null
        );
   
}
