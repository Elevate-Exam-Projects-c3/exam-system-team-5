namespace exam_system.Features.Quizzes.AdminManageQuestions.ViewModels
{
    public class AddQuestionViewModel
    {
        public Guid QuizId { get; set; }

        public string Text { get; set; } = string.Empty;

        public string? Explanation { get; set; }

        public int OrderIndex { get; set; }

        public List<OptionViewModel> Options { get; set; } = new();
    }

    public class OptionViewModel
    {
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
