using MediatR;

using static exam_system.Features.Quizzes.AdminManageQuestions.Commands.UpdateQuestionOrchestrator;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Commands
{
    public record UpdateQuestionOrchestrator(
        Guid QuestionId,
        string Text,
        string? Explanation,
        int OrderIndex,
        IReadOnlyCollection<UpdateOption> Options
    ) : IRequest<Unit>
    {
        public record UpdateOption(
            string OptionText,
            bool IsCorrect
        );
    }
}
