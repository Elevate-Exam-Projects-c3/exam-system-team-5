using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitAttempt.Commands
{
    public record SubmitAttemptCommand(
        Guid AttemptId,
        double? Score
        ) : IRequest<RequestResponse<bool>>;
}
