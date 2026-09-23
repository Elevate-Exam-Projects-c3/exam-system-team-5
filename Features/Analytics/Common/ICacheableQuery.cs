using MediatR;

namespace exam_system.Features.Analytics.Common
{
    public interface ICacheableQuery<TResponse> : IRequest<TResponse>
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
