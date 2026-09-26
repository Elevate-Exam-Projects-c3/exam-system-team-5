using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;
using MediatR;

namespace exam_system.Infrastructure.BackgroundJobs
{
    public class ExpiredAttemptsSweepJob
    {
        private readonly IMediator _mediator;
        public ExpiredAttemptsSweepJob(IMediator mediator) => _mediator = mediator;

        public async Task ExecuteAsync()
        {
            // get alll expired attempts and calculate score and submit them
            var expiredResult = await _mediator.Send(new GetExpiredInProgressAttemptsQuery());

            foreach (var attemptId in expiredResult.Data ?? new List<Guid>())
            {
                await _mediator.Send(new CloseExpiredAttemptOrchestrator(attemptId));
               
            }
        }
    }
}
