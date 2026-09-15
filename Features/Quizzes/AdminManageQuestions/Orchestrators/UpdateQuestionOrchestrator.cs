using exam_system.Features.Shared;
using MediatR;

using static exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators.UpdateQuestionOrchestrator;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators
{
    public record UpdateQuestionOrchestrator(
        Guid QuestionId,
        string Text,
        string? Explanation,
        int OrderIndex,
        IReadOnlyCollection<UpdateOption> Options
    ) : IRequest<RequestResponse<Unit>>
    {
        public record UpdateOption(
            string OptionText,
            bool IsCorrect
        );
    }
}

