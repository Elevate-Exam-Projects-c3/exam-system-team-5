namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos
{
    public class OptionDto
    {
        public Guid Id { get; set; }
        public bool? IsCorrect { get; set; }

        public Guid? SelectedOptionId { get; set; }

    }
}