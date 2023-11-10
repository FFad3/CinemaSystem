using CinemaSystem.Application.Abstraction.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CinemaSystem.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? valueAsString = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (valueAsString is null)
            {
                return null;
            }

            T? value = JsonConvert.DeserializeObject<T>(valueAsString);

            await _distributedCache.RefreshAsync(key, cancellationToken);

            return value;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            await SetAsync(key, value, new DistributedCacheEntryOptions(), cancellationToken);
        }

        public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default) where T : class
        {
            string valueAsString = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, valueAsString, options, cancellationToken);
        }

        public async Task SetAsync<T>(string key, T value, Action<DistributedCacheEntryOptions> configure, CancellationToken cancellationToken = default) where T : class
        {
            var options = new DistributedCacheEntryOptions();

            configure.Invoke(options);

            await SetAsync(key, value, options, cancellationToken);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}