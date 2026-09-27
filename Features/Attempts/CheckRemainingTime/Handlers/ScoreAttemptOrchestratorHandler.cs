using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class ScoreAttemptOrchestratorHandler : IRequestHandler<ScoreAttemptOrchestrator, RequestResponse<AttemptScoreDto>>
    {
        public readonly IMediator _mediator;

        public ScoreAttemptOrchestratorHandler(IMediator mediator) 
            => _mediator = mediator;
        public async Task<RequestResponse<AttemptScoreDto>> Handle(ScoreAttemptOrchestrator command, CancellationToken cancellationToken)
        {
            var calcResult = await _mediator.Send(new CalculateAttemptAnswersQuery(command.AttemptId), cancellationToken);
            if (!calcResult.Success)
                return calcResult;

            await _mediator.Send(new UpdateAnswersCorrectnessCommand(calcResult.Data.Answers), cancellationToken);

            return calcResult;

        }
    }
}
