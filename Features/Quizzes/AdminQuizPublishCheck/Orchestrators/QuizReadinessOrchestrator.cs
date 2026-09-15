using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Orchestrators
{
    public record QuizReadinessOrchestrator(
        Guid QuizId
    ) : IRequest<RequestResponse<QuizReadinessResponse>>;
}
