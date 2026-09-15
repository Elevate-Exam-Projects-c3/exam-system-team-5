using MediatR;
using static exam_system.Features.Quizzes.AdminManageQuestions.Commands.AddQuestionOrchestrator;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Commands
{
    public record AddQuestionOrchestrator(
        Guid QuizId,
        string Text,
        string? Explanation,
        int OrderIndex,
        IReadOnlyCollection<AddOption> Options
    ) : IRequest<Unit>
    {
        public record AddOption(
            string OptionText,
            bool IsCorrect
        );
    }
}
