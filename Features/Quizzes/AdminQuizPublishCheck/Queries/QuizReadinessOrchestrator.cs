using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using MediatR;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries
{
    public record QuizReadinessOrchestrator(
        Guid QuizId
    ) : IRequest<QuizReadinessResponse>;
}
