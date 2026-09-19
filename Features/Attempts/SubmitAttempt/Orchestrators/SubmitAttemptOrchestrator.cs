using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitAttempt.Orchestrators
{
    public record SubmitAttemptOrchestrator(Guid AttemptId) : IRequest<RequestResponse<bool>>;
  
}
