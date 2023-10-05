using CinemaSystem.Core.Repositories;
using CinemaSystem.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystem.Infrastructure.DAL
{
    internal static class Extension
    {
        private const string OptionsSectionName = "SQLdb";
        internal static IServiceCollection AddSql(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<SQLOptions>(configuration);
            var options = configuration.GetOptions<SQLOptions>(OptionsSectionName);

            services.AddDbContext<CinemaSystemDbContext>(cfg =>
            {
                if(options.UseInMemory)
                {
                    cfg.UseInMemoryDatabase("CinemaSystemDb");
                }
                else
                {
                    cfg.UseSqlServer(options.ConnectionString);
                }

            });
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
