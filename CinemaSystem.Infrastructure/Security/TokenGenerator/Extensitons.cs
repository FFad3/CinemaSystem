using CinemaSystem.Application.Abstraction.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.Security.TokenGenerator
{
    public static class Extensitons
    {
        public const string SectionName = "JWT";

        public static IServiceCollection AddJwt(this IServiceCollection services , IConfiguration configuration)
        {
            services.Configure<TokenGeneratorSettings>(configuration);
            services.AddSingleton<ITokenGenarator, TokenGenerator>();

            return services;
        }
    }
}