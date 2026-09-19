using exam_system.Features.Attempts.SubmitAttempt.Commands;
using exam_system.Features.Attempts.SubmitAttempt.Orchestrators;
using exam_system.Features.Attempts.SubmitAttempt.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public class SubmitAttemptOrchestratorHandler(IMediator mediator) : IRequestHandler<SubmitAttemptOrchestrator, RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(SubmitAttemptOrchestrator request, CancellationToken cancellationToken)
        {
            var AttemptScore = await mediator.Send(new GetAttemptScoreQuery(request.AttemptId), cancellationToken);
            var result = await mediator.Send(new SubmitAttemptCommand(request.AttemptId, AttemptScore.Data), cancellationToken);
            return result;
        }
    }
}
