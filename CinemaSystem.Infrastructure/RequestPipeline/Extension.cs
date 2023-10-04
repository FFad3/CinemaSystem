using CinemaSystem.Application.Abstraction.Requestsr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Application.Abstraction.Requests
{
    internal static class Extension
    {
        internal static IServiceCollection ConfigureRequestPipeline(this IServiceCollection services, Action<PipelineConfiguration>? configuration = null)
        {
            var settings = new PipelineConfiguration();
            configuration?.Invoke(settings);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            if (settings.IsPereformenceLoggingActive)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PereformenceBehaviour<,>));
            }
            if (settings.IsRequestPayloadLoggingActive)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));
            }
            if (settings.IsRequestResultLoggingActive)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResultLoggingBehaviour<,>));
            }
            return services;
        }

        internal sealed class PipelineConfiguration
        {
            public bool DisablePereformenceLogging { get; set; } = false;
            public bool DisableRequestPayloadLogging { get; set; } = false;
            public bool DisableRequestResultLogging { get; set; } = false;

            public bool IsPereformenceLoggingActive => !DisablePereformenceLogging;
            public bool IsRequestPayloadLoggingActive => !DisableRequestPayloadLogging;
            public bool IsRequestResultLoggingActive => !DisableRequestResultLogging;
        }
    }
}