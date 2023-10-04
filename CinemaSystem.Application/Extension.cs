using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies();
            });

            return services;
        }
    }
}