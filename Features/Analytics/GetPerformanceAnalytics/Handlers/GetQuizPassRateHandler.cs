using Dapper;
using exam_system.Common.Enums;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using MediatR;
using System.Data;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetQuizPassRateHandler : IRequestHandler<GetQuizPassRateQuery, IReadOnlyList<QuizPassRateDto>>
{
    private readonly IDbConnection _db;

    public GetQuizPassRateHandler(IDbConnection db)
       => _db = db;

    public async Task<IReadOnlyList<QuizPassRateDto>> Handle(GetQuizPassRateQuery request, CancellationToken cancellationToken)
    {
        var f = request.Filter;
        var dateToExclusive = f.DateTo?.Date.AddDays(1);
        const string sql = @"
           SELECT 
                q.Id AS QuizId,
                q.Title AS QuizTitle,
                COUNT(a.Id) AS TotalAttempts,
                COUNT(CASE WHEN a.Score >= q.PassScore THEN 1 END) AS PassedAttempts,
                CAST(
                    ROUND((CAST(COUNT(CASE WHEN a.Score >= q.PassScore THEN 1 END) AS FLOAT) / COUNT(a.Id)) * 100.0, 2)
                AS FLOAT) AS PassRatePercentage
            FROM Quizzes q
            JOIN QuizAttempts a ON q.Id = a.QuizId AND a.Status = @CompletedStatus
            WHERE (@DiplomaId IS NULL OR q.DiplomaId = @DiplomaId)
              AND (@DateFrom IS NULL OR a.StartTime >= @DateFrom)
              AND (@DateTo IS NULL OR a.StartTime < @DateTo)
            GROUP BY q.Id, q.Title, q.PassScore
            HAVING COUNT(a.Id) > 0
            ORDER BY PassRatePercentage DESC;";

        var command = new CommandDefinition(sql, new
        {
            f.DiplomaId,
            f.DateFrom,
            DateTo = dateToExclusive,
            CompletedStatus = (int)AttemptStatus.Submitted
        }, cancellationToken: cancellationToken);

        var results = await _db.QueryAsync<QuizPassRateDto>(command);
        return results.ToList().AsReadOnly();
    }
}