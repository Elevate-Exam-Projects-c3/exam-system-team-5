using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Commands
{
    public record AddQuestionCommand(
        Guid QuestionId,
        Guid QuizId,
        string Text,
        string? Explanation,
        int OrderIndex
    ) : IRequest<Unit>;
}
