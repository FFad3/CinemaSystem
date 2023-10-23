using System.Text;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Core.Entities;
using CinemaSystem.Infrastructure.Security.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CinemaSystem.Infrastructure.Security
{
    internal static class Extenstion
    {
        internal static IServiceCollection AddSeciurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationConfiguration>(configuration.GetRequiredSection(AuthenticationConfiguration.SectionName));
            var config = configuration.GetOptions<AuthenticationConfiguration>(AuthenticationConfiguration.SectionName);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.AccessTokenSecret)),
                    ValidIssuer = config.Issuer,
                    ValidAudience = config.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthentication();

            services
                .AddSingleton<TokenGenerator>()
                .AddSingleton<AccessTokenGenerator>()
                .AddSingleton<RefreshTokenGenerator>()
                .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddTransient<IPasswordManager, PasswordManager>()
                .AddTransient<ITokenStorage, HttpContextTokenStorage>()
                .AddTransient<IAuthenticator, Authenticator>();
            ;

            return services;
        }
    }
}