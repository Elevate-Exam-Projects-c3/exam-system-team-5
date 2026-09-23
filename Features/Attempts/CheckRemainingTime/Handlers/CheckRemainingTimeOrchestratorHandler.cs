using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class CheckRemainingTimeOrchestratorHandler : IRequestHandler<CheckRemainingTimeOrchestrator, RequestResponse<TimeRemainingResponseDto>>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;

        public CheckRemainingTimeOrchestratorHandler(IMediator mediator, IGenericRepository<QuizAttempt> attemptRepository)
        {
            _mediator = mediator;
            _attemptRepository = attemptRepository;
        }

        public async Task<RequestResponse<TimeRemainingResponseDto>> Handle(CheckRemainingTimeOrchestrator request, CancellationToken cancellationToken)
        {
            // خطوة 1: هات المحاولة، وتأكدي إنها بتاعت نفس الطالب اللي بيسأل
            var attempt = await _attemptRepository.Get(a => a.Id == request.AttemptId).FirstOrDefaultAsync(cancellationToken);
            if (attempt is null)
                return RequestResponse<TimeRemainingResponseDto>.Fail("Attempt not found.", 404);

            if (attempt.StudentId != request.StudentId)
                return RequestResponse<TimeRemainingResponseDto>.Fail("You do not own this attempt.", 403);


            //if (attempt.Status != AttemptStatus.InProgress)
            //    return RequestResponse<TimeRemainingResponseDto>.Ok(attempt);

            // خطوة 3: لسه InProgress — احسبي الفرق
            var remaining = attempt.Deadline - DateTime.UtcNow;
            //if (remaining > TimeSpan.Zero)
            //    return RequestResponse<TimeRemainingResponseDto>.Ok(new TimeRemainingResponseDto
            //    {
            //        Status = "InProgress",
            //        RemainingSeconds = (int)remaining.TotalSeconds
            //    });

            await _mediator.Send(new AutoSubmitExpiredAttemptCommand(request.AttemptId), cancellationToken);

            var reloaded = await _attemptRepository.Get(a => a.Id == request.AttemptId).FirstAsync(cancellationToken);
            return RequestResponse<TimeRemainingResponseDto>.Ok();
        }
    }
}
