using MediatR;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrators
{
    public record UnpublishQuizOrchestrator(Guid QuizId)
        : IRequest<Unit>;
}
