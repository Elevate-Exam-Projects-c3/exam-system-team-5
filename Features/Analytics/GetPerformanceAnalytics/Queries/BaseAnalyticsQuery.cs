using exam_system.Features.Analytics.Common;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public abstract record BaseAnalyticsQuery<TResponse>(AnalyticsFilterDto Filter) : ICacheableQuery<TResponse>
    {
        protected abstract string FeatureName { get; }
        public string CacheKey
        {
            get
            {
                var diploma = Filter.DiplomaId?.ToString("N") ?? "all";
                var from = Filter.DateFrom?.ToString("yyyyMMdd_HHmmss") ?? "min";
                var to = Filter.DateTo?.ToString("yyyyMMdd_HHmmss") ?? "max";
                return $"analytics_{FeatureName}_{diploma}_{from}_{to}";
            }
        }

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
