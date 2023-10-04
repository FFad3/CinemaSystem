using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.Security
{
    internal static class Extenstion
    {
        internal static IServiceCollection AddSeciurity(this IServiceCollection services)
        {
            services
                .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddTransient<IPasswordManager, PasswordManager>()
            ;

            return services;
        }
    }
}