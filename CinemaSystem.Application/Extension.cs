using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currrentAssembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(currrentAssembly);
            services.AddAutoMapper(currrentAssembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(currrentAssembly);
            });

            return services;
        }
    }
}