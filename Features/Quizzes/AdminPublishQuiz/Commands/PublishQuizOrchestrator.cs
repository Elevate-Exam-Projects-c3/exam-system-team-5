using MediatR;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Commands
{
    public record PublishQuizOrchestrator(Guid QuizId) : IRequest<Unit>;

}
