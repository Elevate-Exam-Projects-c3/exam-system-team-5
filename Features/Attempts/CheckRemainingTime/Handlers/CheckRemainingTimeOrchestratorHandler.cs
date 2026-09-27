using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Common.Attempts.GetAttemptById.Query;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class CheckRemainingTimeOrchestratorHandler : IRequestHandler<CheckRemainingTimeOrchestrator, RequestResponse<TimeRemainingResponseDto>>
    {
        private readonly IMediator _mediator;

        public CheckRemainingTimeOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RequestResponse<TimeRemainingResponseDto>> Handle(CheckRemainingTimeOrchestrator request, CancellationToken cancellationToken)
        {
            var attemptResult = await _mediator.Send(new GetAttemptByIdQuery(request.AttemptId), cancellationToken);
            if (!attemptResult.Success)
                return RequestResponse<TimeRemainingResponseDto>.Fail(attemptResult.Message, attemptResult.StatusCode);

            var attempt = attemptResult.Data;

            //check ownership of the attempt 
            if (attempt.StudentId != request.StudentId)
                return RequestResponse<TimeRemainingResponseDto>.Fail("You do not own this attempt.", 403);

            //check if the attempt status now
            if (attempt.Status != AttemptStatus.InProgress)
                return RequestResponse<TimeRemainingResponseDto>.Ok(new TimeRemainingResponseDto(attempt.Status.ToString(), null));

            // calculate the remaining time if the attempt is still in progress
            var remaining = attempt.Deadline - DateTime.UtcNow;
            if (remaining > TimeSpan.Zero)
                return RequestResponse<TimeRemainingResponseDto>.Ok(new TimeRemainingResponseDto("InProgress", (int)remaining.TotalSeconds));

            var closeResult = await _mediator.Send(new CloseExpiredAttemptOrchestrator(request.AttemptId), cancellationToken);
            if (!closeResult.Success)
                return RequestResponse<TimeRemainingResponseDto>.Fail(closeResult.Message, closeResult.StatusCode);

            return RequestResponse<TimeRemainingResponseDto>.Ok(new TimeRemainingResponseDto("TimedOut", null));
        }
    }
}
