using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptHistory.Models;
using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.GetAttemptHistory.Handlers
{
    public class GetAttemptHistoryQueryHandler(IGenericRepository<QuizAttempt> attemptRepository) : IRequestHandler<GetAttemptHistoryQuery,RequestResponse<PaginatedResult<AttemptHistoryResponse>>>
    {
        public async Task<RequestResponse<PaginatedResult<AttemptHistoryResponse>>> Handle(
            GetAttemptHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var pageIndex = request.PageIndex < 1? 1: request.PageIndex;

            var pageSize = request.PageSize < 1? 10: request.PageSize;

            var query = attemptRepository.GetAll()
                .Where(a => a.StudentId == request.StudentId);

            var totalCount = await query.CountAsync(cancellationToken);

            var attempts = await query
                .OrderByDescending(a => a.SubmittedAt ?? a.StartTime)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AttemptHistoryResponse(
                    a.Id,
                    a.QuizId,
                    a.Quiz.Title,
                    a.Status,
                    a.Score,
                    a.StartTime,
                    a.SubmittedAt
                ))
                .ToListAsync(cancellationToken);

            var result = PaginatedResult<AttemptHistoryResponse>.Create(attempts,totalCount,pageIndex,pageSize);

            return RequestResponse<PaginatedResult<AttemptHistoryResponse>>.Ok(result,"Attempt history retrieved successfully");
        }
    }
}
