using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Commands
{

    public record ScoreAttemptCommand(Guid AttemptId) : IRequest<RequestResponse<AttemptScoreDto>>;

}
