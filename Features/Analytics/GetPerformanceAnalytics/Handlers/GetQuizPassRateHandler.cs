using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Extensions;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetQuizPassRateHandler : IRequestHandler<GetQuizPassRateQuery, IReadOnlyList<QuizPassRateDto>>
{
    private readonly IGenericRepository<QuizAttempt> _attemptRepo;

    public GetQuizPassRateHandler(IGenericRepository<QuizAttempt> attemptRepo)
        => _attemptRepo = attemptRepo;

    public async Task<IReadOnlyList<QuizPassRateDto>> Handle(
        GetQuizPassRateQuery request,
        CancellationToken cancellationToken)
    {
        var groupedData = await _attemptRepo.GetAll()
            .AsNoTracking()
            .Where(a => a.Status == AttemptStatus.Submitted)
            .ApplyAnalyticsFilter(request.DateFrom, request.DateTo, request.DiplomaId)
            .Select(a => new
            {
                a.QuizId,
                QuizTitle = a.Quiz.Title,
                IsPassed = a.Score >= a.Quiz.PassScore ? 1 : 0
            })
            .GroupBy(x => new { x.QuizId, x.QuizTitle })
            .Select(g => new
            {
                g.Key.QuizId,
                g.Key.QuizTitle,
                TotalAttempts = g.Count(),
                PassedAttempts = g.Sum(x => x.IsPassed)
            })
            .ToListAsync(cancellationToken);

        var result = groupedData
            .Select(x => new QuizPassRateDto(
                x.QuizId,
                x.QuizTitle,
                x.TotalAttempts,
                x.PassedAttempts,
                x.TotalAttempts > 0
                    ? Math.Round((double)x.PassedAttempts / x.TotalAttempts * 100.0, 2)
                    : 0.0
            ))
            .OrderByDescending(x => x.PassRatePercentage)
            .ToList();

        return result;
    }
}