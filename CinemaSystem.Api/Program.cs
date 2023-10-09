using CinemaSystem.Application;
using CinemaSystem.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CinemaSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //SetUp SeriLog as logger
            var seriLogConfiguraiton = new ConfigurationBuilder()
                        .AddJsonFile("seri-log.config.json")
                        .Build();

            builder.Host.UseSerilog((_, configuration) =>
                configuration.ReadFrom.Configuration(seriLogConfiguraiton));
           
            var cfg = builder.Configuration;
            // Add services to the container.
            builder.Services
                .AddApplication()
                .AddInfrastructure(cfg);

            var app = builder.Build();

            app.UseInfrastructure();

            app.Run();
        }
    }
}