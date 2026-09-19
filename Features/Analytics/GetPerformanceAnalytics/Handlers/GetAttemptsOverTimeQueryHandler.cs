using Dapper;
using exam_system.Common.Enums;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers
{
    public class GetAttemptsOverTimeQueryHandler : IRequestHandler<GetAttemptsOverTimeQuery, IReadOnlyList<AttemptsOverTimeDto>>
    {
        private readonly IDbConnection _db;

        public GetAttemptsOverTimeQueryHandler(IDbConnection db, IMemoryCache cache)
           => _db = db;
        public async Task<IReadOnlyList<AttemptsOverTimeDto>> Handle(GetAttemptsOverTimeQuery request, CancellationToken cancellationToken)
        {
            var f = request.Filter;
            var dateToExclusive = f.DateTo?.Date.AddDays(1);
            const string sql = @"
            SELECT 
                CAST(a.StartTime AS DATE) AS [Date],
                COUNT(a.Id) AS AttemptCount
            FROM QuizAttempts a
            JOIN Quizzes q ON a.QuizId = q.Id
            WHERE a.Status = @CompletedStatus
              AND (@DiplomaId IS NULL OR q.DiplomaId = @DiplomaId)
              AND (@DateFrom IS NULL OR a.StartTime >= @DateFrom)
              AND (@DateTo IS NULL OR a.StartTime < @DateTo)
            GROUP BY CAST(a.StartTime AS DATE)
            ORDER BY [Date] ASC;";
            var command = new CommandDefinition(sql, new
            {
                f.DiplomaId,
                f.DateFrom,
                DateTo = dateToExclusive,
                CompletedStatus = (int)AttemptStatus.Submitted
            }, cancellationToken: cancellationToken);

            var results = await _db.QueryAsync<AttemptsOverTimeDto>(command);
            return results.ToList().AsReadOnly();
        }
    }
}
