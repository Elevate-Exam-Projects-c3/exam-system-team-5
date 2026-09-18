using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries
{
    public record CheckInProgressAttemptsQuery(Guid QuizId)
        : IRequest<RequestResponse<Unit>>;
}
