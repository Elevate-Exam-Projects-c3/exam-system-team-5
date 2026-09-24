using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Common.Attempts.GetAttemptById.Dtos;
using exam_system.Features.Common.Attempts.GetAttemptById.Query;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Common.Attempts.GetAttemptById.Handler
{
    public class GetAttemptByIdQueryHandler : IRequestHandler<GetAttemptByIdQuery, RequestResponse<AttemptSummaryDto>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public GetAttemptByIdQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<AttemptSummaryDto>> Handle(GetAttemptByIdQuery request, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.Get(a => a.Id == request.AttemptId)
                .Select(a => new AttemptSummaryDto(a.Id, a.StudentId, a.Status, a.Deadline))
                .FirstOrDefaultAsync(cancellationToken);

            return attempt is null
                ? RequestResponse<AttemptSummaryDto>.Fail("Attempt not found.", 404)
                : RequestResponse<AttemptSummaryDto>.Ok(attempt);
        }
    }
}
