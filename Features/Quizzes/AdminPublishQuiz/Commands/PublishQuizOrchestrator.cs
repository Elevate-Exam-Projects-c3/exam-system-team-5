using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Commands
{
    public record PublishQuizOrchestrator(Guid QuizId) : IRequest<RequestResponse<Unit>>;

}
