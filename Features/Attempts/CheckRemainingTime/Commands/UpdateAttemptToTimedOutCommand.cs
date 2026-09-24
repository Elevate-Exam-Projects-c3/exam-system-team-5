using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Commands
{
    public record UpdateAttemptToTimedOutCommand(Guid AttemptId, decimal Score, bool Passed): IRequest<RequestResponse<Unit>>;
}
