﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal static class Extension
    {
        
        internal static IServiceCollection ConfigureMediatRPipeline(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetOptions<PipelineConfiguration>(PipelineConfiguration.SectionName);

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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            if (settings.RequestResultLogging)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResultLoggingBehaviour<,>));
            }
            return services;
        }

        internal sealed class PipelineConfiguration
        {
            public const string SectionName = "MediatRPieline";
            public bool PereformenceLogging { get; set; } = false;
            public bool RequestPayloadLogging { get; set; } = false;
            public bool RequestResultLogging { get; set; } = false;
        }
    }
}