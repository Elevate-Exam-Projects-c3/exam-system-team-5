using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Extensions;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers
{
    public class GetTopFailedQuestionsHandler : IRequestHandler<GetTopFailedQuestionsQuery, IReadOnlyList<TopFailedQuestionDto>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepo;

        public GetTopFailedQuestionsHandler(IGenericRepository<QuizAttempt> attemptRepo)
            => _attemptRepo = attemptRepo;
        public async Task<IReadOnlyList<TopFailedQuestionDto>> Handle(GetTopFailedQuestionsQuery request, CancellationToken cancellationToken)
        {
            var groupedData = await _attemptRepo.GetAll()
            .AsNoTracking()
            .Where(a => a.Status == AttemptStatus.Submitted)
            .ApplyAnalyticsFilter(request.DateFrom, request.DateTo, request.DiplomaId)
            .SelectMany(a => a.Answers)
            .Select(ans => new
            {
                ans.QuestionId,
                QuestionText = ans.Question.Text,
                QuizTitle = ans.Attempt.Quiz.Title,
                IsCorrectInt = ans.IsCorrect == true ? 1 : 0
            })
            .GroupBy(x => new
            {
                x.QuestionId,
                x.QuestionText,
                x.QuizTitle
            })
            .Select(g => new
            {
                g.Key.QuestionId,
                g.Key.QuestionText,
                g.Key.QuizTitle,
                TotalAnswers = g.Count(),
                CorrectAnswers = g.Sum(x => x.IsCorrectInt)
            })
            .ToListAsync(cancellationToken);

            var result = groupedData
                .Select(x =>
                {
                    var total = x.TotalAnswers;
                    var incorrect = total - x.CorrectAnswers;
                    var successRate = total > 0 ? ((double)x.CorrectAnswers / total * 100.0) : 0.0;
                    var failureRate = total > 0 ? Math.Round((double)incorrect / total * 100.0, 2) : 0.0;

                    return new
                    {
                        x.QuestionId,
                        x.QuestionText,
                        x.QuizTitle,
                        TotalAnswers = total,
                        IncorrectAnswers = incorrect,
                        SuccessRate = successRate,
                        FailureRatePercentage = failureRate
                    };
                })
                .Where(x => x.TotalAnswers >= request.MinAnswers && x.SuccessRate <= request.MaxSuccessRate)
                .OrderByDescending(x => x.FailureRatePercentage)
                .Take(request.Take)
                .Select(x => new TopFailedQuestionDto(
                    x.QuestionId,
                    x.QuestionText,
                    x.QuizTitle,
                    x.TotalAnswers,
                    x.IncorrectAnswers,
                    x.FailureRatePercentage
                ))
                .ToList();

            return result;
        }
    }
}
