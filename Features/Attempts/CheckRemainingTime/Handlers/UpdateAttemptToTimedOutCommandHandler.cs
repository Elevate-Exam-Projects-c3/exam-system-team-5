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
        public Task<RequestResponse<Unit>> Handle(UpdateAttemptToTimedOutCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
