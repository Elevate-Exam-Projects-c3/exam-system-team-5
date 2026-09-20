using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Extensions
{
    public static class AnalyticsFilterExtensions
    {
        public static IQueryable<QuizAttempt> ApplyAnalyticsFilter(this IQueryable<QuizAttempt> query, DateTime? dateFrom,
        DateTime? dateTo, Guid? diplomaId)
        {
         
            var dateToExclusive = dateTo?.Date.AddDays(1);
            if (diplomaId.HasValue)
                query = query.Where(a => a.Quiz.DiplomaId == diplomaId.Value);
            if (dateFrom.HasValue)
                query = query.Where(a => a.StartTime >= dateFrom.Value);
            if (dateToExclusive.HasValue)
                query = query.Where(a => a.StartTime < dateToExclusive.Value);
            return query;
        }
    }
}
