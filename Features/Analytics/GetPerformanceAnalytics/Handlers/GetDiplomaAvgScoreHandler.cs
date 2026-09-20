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
    public class GetDiplomaAvgScoreHandler : IRequestHandler<GetDiplomaAvgScoreQuery, IReadOnlyList<DiplomaAvgScoreDto>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepo;

        public GetDiplomaAvgScoreHandler(IGenericRepository<QuizAttempt> attemptRepo)
            => _attemptRepo = attemptRepo;
        public async Task<IReadOnlyList<DiplomaAvgScoreDto>> Handle(GetDiplomaAvgScoreQuery request, CancellationToken cancellationToken)
        {
            var groupedData = await _attemptRepo.GetAll()
                .AsNoTracking()
                .Where(a => a.Status == AttemptStatus.Submitted)
                .ApplyAnalyticsFilter(request.DateFrom, request.DateTo, request.DiplomaId)
                .GroupBy(a => new
                {
                    a.Quiz.DiplomaId,
                    a.Quiz.Diploma.Title
                })
                .Select(g => new
                {
                    DiplomaId = g.Key.DiplomaId,
                    DiplomaTitle = g.Key.Title,
                    TotalAttempts = g.Count(),
                    RawAverageScore = g.Average(a => (double)a.Score)
                })
                .ToListAsync(cancellationToken);

            var result = groupedData
                .Select(x => new DiplomaAvgScoreDto(
                    x.DiplomaId,
                    x.DiplomaTitle,
                    Math.Round(x.RawAverageScore, 2),
                    x.TotalAttempts
                ))
                .OrderByDescending(x => x.AverageScore)
                .ToList();

            return result;
        }
    }
}
