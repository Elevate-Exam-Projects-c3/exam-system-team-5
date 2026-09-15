using exam_system.Features.Shared;
using MediatR;
using static exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators.AddQuestionOrchestrator;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators
{
    public record AddQuestionOrchestrator(
        Guid QuizId,
        string Text,
        string? Explanation,
        int OrderIndex,
        IReadOnlyCollection<AddOption> Options
    ) : IRequest<RequestResponse<Unit>>
    {
        public record AddOption(
            string OptionText,
            bool IsCorrect
        );
    }
}
