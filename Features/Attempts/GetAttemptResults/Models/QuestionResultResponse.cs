namespace exam_system.Features.Attempts.GetAttemptResults.Models
{
    public class QuestionResultResponse
    {
        public Guid QuestionId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public string? SelectedAnswer { get; set; }

        public bool IsCorrect { get; set; }

        public string? CorrectAnswer { get; set; }

        public string? Explanation { get; set; }
    }
}
