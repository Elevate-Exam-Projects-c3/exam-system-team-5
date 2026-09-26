using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class GetExpiredInProgressAttemptsQueryHandler : IRequestHandler<GetExpiredInProgressAttemptsQuery, RequestResponse<List<Guid>>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public GetExpiredInProgressAttemptsQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<List<Guid>>> Handle(GetExpiredInProgressAttemptsQuery request, CancellationToken cancellationToken)
        {

            var ids = await _attemptRepository
                .Get(a => a.Status == AttemptStatus.InProgress && a.Deadline < DateTime.UtcNow)
                .Select(a => a.Id)
                .ToListAsync(cancellationToken);

            return RequestResponse<List<Guid>>.Ok(ids);

        }
    }
}
