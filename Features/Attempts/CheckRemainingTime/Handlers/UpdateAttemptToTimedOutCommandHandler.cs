using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class UpdateAttemptToTimedOutCommandHandler : IRequestHandler<UpdateAttemptToTimedOutCommand, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public UpdateAttemptToTimedOutCommandHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<Unit>> Handle(UpdateAttemptToTimedOutCommand command, CancellationToken cancellationToken)
        {
            await _attemptRepository.UpdateAsync(
                    a => a.Id == command.AttemptId && a.Status == AttemptStatus.InProgress,
                    setters => setters
                        .SetProperty(a => a.Status, AttemptStatus.TimedOut)
                        .SetProperty(a => a.Score, command.Score)
                        .SetProperty(a => a.Passed, command.Passed)
                        .SetProperty(a => a.SubmittedAt, DateTime.UtcNow),
                    cancellationToken);

            return RequestResponse<Unit>.Ok(Unit.Value);
        }
    }
}
