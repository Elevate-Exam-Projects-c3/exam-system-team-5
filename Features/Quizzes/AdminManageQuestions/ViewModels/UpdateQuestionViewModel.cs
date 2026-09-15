namespace exam_system.Features.Quizzes.AdminManageQuestions.ViewModels
{
    public class UpdateQuestionViewModel
    {
        public Guid QuestionId { get; set; }

        public string Text { get; set; } = string.Empty;

        public string? Explanation { get; set; }

        public int OrderIndex { get; set; }

        public List<UpdateOptionViewModel> Options { get; set; } = new();
    }

    public class UpdateOptionViewModel
    {
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
