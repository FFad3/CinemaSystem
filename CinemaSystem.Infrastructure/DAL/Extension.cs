using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Infrastructure.DAL.Repositories;
using CinemaSystem.Infrastructure.DAL.Repositories.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.DAL
{
    internal static class Extension
    {
        internal static IServiceCollection AddSql(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<SQLOptions>(configuration);
            var options = configuration.GetOptions<SQLOptions>(SQLOptions.SectionName);

            services.AddDbContext<CinemaSystemDbContext>(cfg =>
            {
                if(options.UseInMemory)
                {
                    cfg.UseInMemoryDatabase(options.InMemoryDbName);
                }
                else
                {
                    cfg.UseSqlServer(options.ConnectionString);
                }

            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            return services;
        }
    }
}
