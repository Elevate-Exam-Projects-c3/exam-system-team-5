namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos
{
    public class GetQuestionOptionsResponse
    {
        public Guid OptionId { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
