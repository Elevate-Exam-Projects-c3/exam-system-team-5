namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos
{
    public class GetQuizByIdResponse
    {
        public Guid Id { get; set; }
        public int DurationMinutes { get; set; }
        public int PassScore { get; set; }
    }
}
