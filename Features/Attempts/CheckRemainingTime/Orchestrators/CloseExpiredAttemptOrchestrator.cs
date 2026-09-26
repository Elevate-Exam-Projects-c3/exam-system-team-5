using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Orchestrators
{
    public record CloseExpiredAttemptOrchestrator(Guid AttemptId) : IRequest<RequestResponse<Unit>>;
}
