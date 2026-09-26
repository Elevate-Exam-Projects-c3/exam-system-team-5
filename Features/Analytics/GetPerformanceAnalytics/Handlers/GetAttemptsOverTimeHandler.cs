using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Extensions;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetAttemptsOverTimeHandler : IRequestHandler<GetAttemptsOverTimeQuery, IReadOnlyList<AttemptsOverTimeDto>>
{
    private readonly IGenericRepository<QuizAttempt> _attemptRepo;

    public GetAttemptsOverTimeHandler(IGenericRepository<QuizAttempt> attemptRepo)
        => _attemptRepo = attemptRepo;

    public async Task<IReadOnlyList<AttemptsOverTimeDto>> Handle(
        GetAttemptsOverTimeQuery request,
        CancellationToken cancellationToken)
    {
        var groupedData = await _attemptRepo.GetAll()
            .AsNoTracking()
            .Where(a => a.Status == AttemptStatus.Submitted)
            .ApplyAnalyticsFilter(request.DateFrom, request.DateTo, request.DiplomaId)
            .GroupBy(a => a.StartTime.Date)
            .Select(g => new
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToListAsync(cancellationToken);

        var result = groupedData
            .Select(x => new AttemptsOverTimeDto(
                x.Date,
                x.Count
            ))
            .ToList();

        return result;
    }
}