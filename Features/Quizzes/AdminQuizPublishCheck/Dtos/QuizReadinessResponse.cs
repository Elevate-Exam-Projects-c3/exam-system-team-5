namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos
{
    public class QuizReadinessResponse
    {
        public Guid QuizId { get; set; }
        public bool IsReady { get; set; }
        public List<ReadinessCheck> Checks { get; set; } = new();
    }

    public class ReadinessCheck
    {
        public string Name { get; set; } = string.Empty;
        public bool Passed { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
