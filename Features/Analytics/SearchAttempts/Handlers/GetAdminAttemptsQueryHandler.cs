using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.SearchAttempts.DTOs;
using exam_system.Features.Analytics.SearchAttempts.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Analytics.SearchAttempts.Handlers
{
    public class GetAdminAttemptsQueryHandler : IRequestHandler<GetAdminAttemptsQuery, PaginatedResult<AdminAttemptDto>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;

        public GetAdminAttemptsQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<PaginatedResult<AdminAttemptDto>> Handle(GetAdminAttemptsQuery request, CancellationToken cancellationToken)
        {
            var query = _attemptRepository.GetAll();

            if (request.QuizId.HasValue)
                query = query.Where(a => a.QuizId == request.QuizId.Value);
            if (request.StudentId.HasValue)
                query = query.Where(a => a.StudentId == request.StudentId.Value);
            if (request.Status.HasValue)
                query = query.Where(a => a.Status == request.Status.Value);
            query = request.SortDescending
                 ? query.OrderByDescending(a => a.SubmittedAt ?? a.StartTime)
                 : query.OrderBy(a => a.SubmittedAt ?? a.StartTime);
            var totalCount = await query.CountAsync(cancellationToken);
            var pageIndex = request.PageIndex < 1 ? 1 : request.PageIndex;
            var pageSize = request.PageIndex < 1 ? 10 : request.PageSize;
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AdminAttemptDto(
                    a.Id,
                    a.QuizId,
                    a.Quiz.Title,
                    a.StudentId,
                    a.Student.StudentCode,
                    a.Student.User.Email ?? string.Empty,
                    a.Student.User.FullName,
                    a.Status,
                    a.StartTime,
                    a.SubmittedAt,
                    a.Deadline,
                    a.Score,
                    a.Passed
                    )).ToListAsync(cancellationToken);
            return PaginatedResult<AdminAttemptDto>.Create(items, totalCount, pageIndex, pageSize);
        }
    }
}
