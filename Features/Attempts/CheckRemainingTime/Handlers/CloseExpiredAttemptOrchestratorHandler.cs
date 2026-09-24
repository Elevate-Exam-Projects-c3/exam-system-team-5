using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class CloseExpiredAttemptOrchestratorHandler : IRequestHandler<CloseExpiredAttemptOrchestrator, RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        public CloseExpiredAttemptOrchestratorHandler(IMediator mediator) 
            => _mediator = mediator;
        public async Task<RequestResponse<Unit>> Handle(CloseExpiredAttemptOrchestrator request, CancellationToken cancellationToken)
        {
            var scoreResult = await _mediator.Send(new ScoreAttemptCommand(request.AttemptId), cancellationToken);
            if (!scoreResult.Success)
                return RequestResponse<Unit>.Fail(scoreResult.Message, scoreResult.StatusCode);

            return await _mediator.Send(
                new UpdateAttemptToTimedOutCommand(request.AttemptId, scoreResult.Data!.Score, scoreResult.Data.Passed),
                cancellationToken);
        }
    }
}
