using Dapper;
using exam_system.Common.Enums;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using MediatR;
using System.Data;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers
{
    public class GetDiplomaAvgScoreHandler : IRequestHandler<GetDiplomaAvgScoreQuery, IReadOnlyList<DiplomaAvgScoreDto>>
    {
        private readonly IDbConnection _db;
        public GetDiplomaAvgScoreHandler(IDbConnection db)
           =>  _db = db;
        public async Task<IReadOnlyList<DiplomaAvgScoreDto>> Handle(GetDiplomaAvgScoreQuery request, CancellationToken cancellationToken)
        {
            var f = request.Filter;
            var dateToExclusive = f.DateTo?.Date.AddDays(1);
            const string sql = @"
            SELECT 
                d.Id AS DiplomaId,
                d.Title AS DiplomaTitle,
                CAST(ISNULL(ROUND(AVG(CAST(a.Score AS FLOAT)), 2), 0.0) AS FLOAT) AS AverageScore,
                COUNT(a.Id) AS TotalAttempts
            FROM Diplomas d
            LEFT JOIN Quizzes q ON d.Id = q.DiplomaId
            LEFT JOIN QuizAttempts a ON q.Id = a.QuizId 
                AND a.Status = @CompletedStatus
                AND (@DateFrom IS NULL OR a.StartTime >= @DateFrom)
                AND (@DateTo IS NULL OR a.StartTime < @DateTo)
            WHERE (@DiplomaId IS NULL OR d.Id = @DiplomaId)
            GROUP BY d.Id, d.Title
            ORDER BY d.Title ASC;";
            var command = new CommandDefinition(sql, new
            {
                f.DiplomaId,
                f.DateFrom,
                DateTo = dateToExclusive,
                CompletedStatus = (int)AttemptStatus.Submitted
            }, cancellationToken: cancellationToken);

            var results = await _db.QueryAsync<DiplomaAvgScoreDto>(command);
            return results.ToList().AsReadOnly();
        }
    }
}
