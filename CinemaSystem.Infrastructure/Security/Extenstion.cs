using System.Text;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Core.Entities;
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
            services.Configure<JwtSettings>(configuration.GetRequiredSection(JwtSettings.SectionName));
            var config = configuration.GetOptions<JwtSettings>(JwtSettings.SectionName);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config.Issuer,
                    ValidAudience = config.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SigningKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddAuthentication();
            
            services
                .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddTransient<IPasswordManager, PasswordManager>()
                .AddTransient<ITokenStorage,HttpContextTokenStorage>()
                .AddSingleton<ITokenGenarator, JwtTokenGenerator>();
            ;

            return services;
        }
    }
}