using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Handlers
{
    public class CheckInProgressAttemptsQueryHandler
        : IRequestHandler<CheckInProgressAttemptsQuery, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;

        public CheckInProgressAttemptsQueryHandler(
            IGenericRepository<QuizAttempt> attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }

        public async Task<RequestResponse<Unit>> Handle(
            CheckInProgressAttemptsQuery request,
            CancellationToken cancellationToken)
        {
            var count = await _attemptRepository.CountAsync(
                attempt =>
                    attempt.QuizId == request.QuizId &&
                    attempt.Status == AttemptStatus.InProgress &&
                    !attempt.IsDeleted);

            if (count > 0)
            {
                throw new ConflictException(
                    "Cannot unpublish quiz while students have in-progress attempts.");
            }

            return RequestResponse<Unit>.Ok(Unit.Value, "No in-progress attempts found.");
        }
    }
}
