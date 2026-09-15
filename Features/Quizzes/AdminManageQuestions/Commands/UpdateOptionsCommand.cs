using MediatR;
using static exam_system.Features.Quizzes.AdminManageQuestions.Commands.UpdateOptionsCommand;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Commands
{
    public record UpdateOptionsCommand(
        Guid QuestionId,
        IReadOnlyCollection<UpdateOption> Options
    ) : IRequest<Unit>
    {
        public record UpdateOption(
            string OptionText,
            bool IsCorrect
        );
    }
}
