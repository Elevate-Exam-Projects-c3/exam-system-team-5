using exam_system.Features.Analytics.Common;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace exam_system.Features.Analytics.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery<TResponse>
    {
        private readonly IMemoryCache _cache;

        public CachingBehavior(IMemoryCache cache)

          => _cache = cache;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(request.CacheKey, out TResponse? cachedResult) && cachedResult != null)
                return cachedResult;

            var reponse = await next();
            if (reponse != null)
            {
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = request.Expiration ?? TimeSpan.FromMinutes(10)
                };
                _cache.Set(request.CacheKey, reponse, options);
            }
            return reponse;
        }

    }
}
