using CinemaSystem.Application.Abstraction.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.Cache
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}