using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Commands
{
    public record UpdateQuestionCommand(
        Guid QuestionId,
        string Text,
        string? Explanation,
        int OrderIndex
    ) : IRequest<RequestResponse<Unit>>;
}
