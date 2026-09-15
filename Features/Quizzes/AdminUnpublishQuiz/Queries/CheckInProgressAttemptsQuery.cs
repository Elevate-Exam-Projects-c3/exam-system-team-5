using MediatR;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries
{
    public record CheckInProgressAttemptsQuery(Guid QuizId)
        : IRequest<Unit>;
}
