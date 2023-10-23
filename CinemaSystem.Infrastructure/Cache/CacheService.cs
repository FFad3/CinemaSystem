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
            string valueAsString = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, valueAsString, cancellationToken);
        }

        public async Task RemoveAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}