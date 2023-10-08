using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Infrastructure.DAL;
using CinemaSystem.Infrastructure.Middlewares;
using CinemaSystem.Infrastructure.RequestPipeline;
using CinemaSystem.Infrastructure.Security;
using CinemaSystem.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CinemaSystem.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSeciurity(configuration);
            services.AddSql(configuration);

            services.AddSingleton<IClock, Clock>();

            services.ConfigureMediatRPipeline(configuration);

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CinemaSystem API",
                    Version = "v1"
                });
            });

            services.AddSingleton<ExceptionMiddleware>();
            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);

            section.Bind(options);

            return options;
        }
    }
}