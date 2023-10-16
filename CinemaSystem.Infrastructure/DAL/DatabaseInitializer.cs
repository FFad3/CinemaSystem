using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CinemaSystem.Infrastructure.DAL
{
    internal sealed class DatabaseInitializer : IHostedService
    {
        // Service locator "anti-pattern" (but it depends) :)
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initializing database");

            //Creating scope for dbContext initialization
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CinemaSystemDbContext>();

            //Applying migrations
            _logger.LogInformation("Applying migrations to database");
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Migration applied succesfully");

            //Seeding data here
            await dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Database initialized succesfully");
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}