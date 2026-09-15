using MediatR;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Commands
{
    public record PublishQuizCommand(Guid QuizId) : IRequest<Unit>;
}
