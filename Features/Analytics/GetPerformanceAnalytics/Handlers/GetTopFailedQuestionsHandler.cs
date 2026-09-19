using Dapper;
using exam_system.Common.Enums;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using MediatR;
using System.Data;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers
{
    public class GetTopFailedQuestionsHandler : IRequestHandler<GetTopFailedQuestionsQuery, IReadOnlyList<TopFailedQuestionDto>>
    {
        private readonly IDbConnection _db;

        public GetTopFailedQuestionsHandler(IDbConnection db)
          =>  _db = db;
        public async Task<IReadOnlyList<TopFailedQuestionDto>> Handle(GetTopFailedQuestionsQuery request, CancellationToken cancellationToken)
        {
            var f = request.Filter;
            var dateToExclusive = f.DateTo?.Date.AddDays(1);
            const string sql = @"
            SELECT TOP (@Take)
                q.Id AS QuestionId,
                MAX(q.Text) AS QuestionText,
                qz.Title AS QuizTitle,
                COUNT(ans.Id) AS TotalAnswers,
                COUNT(CASE WHEN ans.IsCorrect = 1 THEN 1 END) AS CorrectAnswers,
                CAST(
                    ROUND((CAST(COUNT(CASE WHEN ans.IsCorrect = 1 THEN 1 END) AS FLOAT) / COUNT(ans.Id)) * 100.0, 2)
                AS FLOAT) AS SuccessRatePercentage
            FROM Questions q
            JOIN Quizzes qz ON q.QuizId = qz.Id
            JOIN StudentQuestionAnswers ans ON q.Id = ans.QuestionId
            JOIN QuizAttempts a ON ans.AttemptId = a.Id AND a.Status = @CompletedStatus
            WHERE (@DiplomaId IS NULL OR qz.DiplomaId = @DiplomaId)
              AND (@DateFrom IS NULL OR a.StartTime >= @DateFrom)
              AND (@DateTo IS NULL OR a.StartTime < @DateTo)
            GROUP BY q.Id, qz.Title
            HAVING COUNT(ans.Id) >= @MinAnswers 
               AND ((CAST(COUNT(CASE WHEN ans.IsCorrect = 1 THEN 1 END) AS FLOAT) / COUNT(ans.Id)) * 100.0) < @Threshold
            ORDER BY SuccessRatePercentage ASC, TotalAnswers DESC;";
            var command = new CommandDefinition(sql, new
            {
                f.DiplomaId,
                f.DateFrom,
                DateTo = dateToExclusive,
                CompletedStatus = (int)AttemptStatus.Submitted,
                request.Take,
                request.MinAnswers,
                Threshold = request.MaxSuccessRate
            }, cancellationToken: cancellationToken);

            var results = await _db.QueryAsync<TopFailedQuestionDto>(command);
            return results.ToList().AsReadOnly();
        }
    }
}
