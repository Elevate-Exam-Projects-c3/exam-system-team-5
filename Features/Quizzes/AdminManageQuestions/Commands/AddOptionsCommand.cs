using MediatR;
using static exam_system.Features.Quizzes.AdminManageQuestions.Commands.AddOptionsCommand;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Commands
{
    public record AddOptionsCommand(
        Guid QuestionId,
        IReadOnlyCollection<AddOption> Options
    ) : IRequest<Unit>
    {
        public record AddOption(
            string OptionText,
            bool IsCorrect
        );
    }
}
