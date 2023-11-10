using Microsoft.Extensions.Caching.Distributed;

namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;

        Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class;

        Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default) where T : class;

        Task SetAsync<T>(string key, T value, Action<DistributedCacheEntryOptions> configure, CancellationToken cancellationToken = default) where T : class;

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}