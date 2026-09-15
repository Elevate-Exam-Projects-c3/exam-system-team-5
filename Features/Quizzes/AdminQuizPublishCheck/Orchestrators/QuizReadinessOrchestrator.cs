using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using MediatR;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Orchestrators
{
    public record QuizReadinessOrchestrator(
        Guid QuizId
    ) : IRequest<QuizReadinessResponse>;
}
