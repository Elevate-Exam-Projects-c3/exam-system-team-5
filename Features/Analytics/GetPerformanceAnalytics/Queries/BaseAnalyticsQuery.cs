using exam_system.Features.Analytics.Common;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries
{
    public abstract record BaseAnalyticsQuery<TResponse>(DateTime? DateFrom, DateTime? DateTo, Guid? DiplomaId) 
        : ICacheableQuery<TResponse>
    {
        protected abstract string FeatureName { get; }
        public string CacheKey
        {
            get
            {
                var diploma = DiplomaId?.ToString("N") ?? "all";
                var from = DateFrom?.ToString("yyyyMMdd_HHmmss") ?? "min";
                var to = DateTo?.ToString("yyyyMMdd_HHmmss") ?? "max";
                return $"analytics_{FeatureName}_{diploma}_{from}_{to}";
            }
        }

        public virtual TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
