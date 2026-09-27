using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Queries
{
    public record CalculateAttemptAnswersQuery(Guid AttemptId) : IRequest<RequestResponse<AttemptScoreDto>>;
}
