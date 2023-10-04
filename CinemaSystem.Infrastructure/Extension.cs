using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Infrastructure.Security;
using CinemaSystem.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure
{
    internal static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClock, Clock>();

            services.AddSeciurity();

            services.ConfigureRequestPipeline(cfg =>
            {
                cfg.DisablePereformenceLogging = false;
                cfg.DisableRequestPayloadLogging = false;
                cfg.DisableRequestResultLogging = false;
            });

            return services;
        }
    }
}