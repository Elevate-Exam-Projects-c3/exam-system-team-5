namespace exam_system.Features.Quizzes.AdminCreateQuiz.DTOS
{
    public class CreateQuizRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Instructions { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PassScore { get; set; } = 60;
        public int? MaxAttempts { get; set; }
    }
}
