using CinemaSystem.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.DAL
{
    internal static class Extension
    {
        internal static IServiceCollection AddSql(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            return services;
        }
    }
}
