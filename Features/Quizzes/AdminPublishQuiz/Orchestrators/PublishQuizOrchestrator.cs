using MediatR;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Orchestrators
{
    public record PublishQuizOrchestrator(Guid QuizId)
        : IRequest<Unit>;
}
