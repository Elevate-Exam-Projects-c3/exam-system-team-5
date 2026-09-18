namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.ViewModels
{
    public class QuizReadinessViewModel
    {
        public Guid QuizId { get; set; }
        public bool IsReady { get; set; }
        public List<ReadinessCheckViewModel> Checks { get; set; } = new();
    }

    public class ReadinessCheckViewModel
    {
        public string Name { get; set; } = string.Empty;
        public bool Passed { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
