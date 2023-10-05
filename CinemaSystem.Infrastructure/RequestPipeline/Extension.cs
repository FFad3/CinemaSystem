using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal static partial class Extension
    {
        internal static IServiceCollection ConfigureRequestPipeline(this IServiceCollection services, PipelineConfiguration config)
        {
            services.ConfigureRequestPipeline(cfg =>
            {
                cfg.PereformenceLogging = config.PereformenceLogging;
                cfg.RequestResultLogging = config.RequestResultLogging;
                cfg.RequestPayloadLogging = config.RequestPayloadLogging;
            });

            return services;
        }

        internal static IServiceCollection ConfigureRequestPipeline(this IServiceCollection services, Action<PipelineConfiguration>? configuration = null)
        {
            var settings = new PipelineConfiguration();
            configuration?.Invoke(settings);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            if (settings.PereformenceLogging)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PereformenceBehaviour<,>));
            }
            if (settings.RequestPayloadLogging)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));
            }
            if (settings.RequestResultLogging)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResultLoggingBehaviour<,>));
            }
            return services;
        }
    }
}