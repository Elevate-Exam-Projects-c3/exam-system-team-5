using exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record GetAttemptByIdQuery(Guid AttemptId) : IRequest<QuizAttemptDto>;
}
