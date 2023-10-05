using CinemaSystem.Application;
using CinemaSystem.Infrastructure;

namespace CinemaStystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
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