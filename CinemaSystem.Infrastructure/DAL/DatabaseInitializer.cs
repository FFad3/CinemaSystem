using System.Data;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;
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

            //For development
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);

            //Applying migrations
            _logger.LogInformation("Applying migrations to database");
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Migration applied succesfully");

            //Seeding data here
            await SeedData(dbContext, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Database initialized succesfully");
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;

        #region Seeding

        private static async Task SeedData(CinemaSystemDbContext dbContext, CancellationToken cancellationToken)
        {
            //select RoleName, ClaimName from Roles JOIN
            //RolesClaims ON ROLES.Id = RolesClaims.RolesId JOIN
            //RoleClaim ON RoleClaim.Id = RolesClaims.ClaimsId;
            await AddClaims(dbContext, Claims, cancellationToken);
            await AddRoles(dbContext, cancellationToken);
            await AddAccounts(dbContext, cancellationToken);
        }

        private static async Task AddRoles(CinemaSystemDbContext dbContext, CancellationToken cancellationToken)
        {
            if (!await dbContext.Roles.AnyAsync(x => x.RoleName == new RoleName("admin"), cancellationToken))
            {
                var role = new Role(Guid.Parse("10000000-0000-0000-0000-000000000000"), "admin");

                var claims = await dbContext.RoleClaim.ToListAsync(cancellationToken);
                role.AddClaim(claims);
                await dbContext.AddAsync(role, cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            if (!await dbContext.Roles.AnyAsync(x => x.RoleName == new RoleName("user"), cancellationToken))
            {
                var role = new Role(Guid.Parse("20000000-0000-0000-0000-000000000000"), "user");

                var claims = await dbContext.RoleClaim.ToListAsync(cancellationToken);
                var userClaims = claims.Where(x => !(x.Id.Value.ToString().EndsWith('0'))).ToList();

                role.AddClaim(userClaims);

                await dbContext.AddAsync(role, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        private static async Task AddAccounts(CinemaSystemDbContext dbContext, CancellationToken cancellationToken)
        {
            if (!await dbContext.Users.AnyAsync(x => x.Username == new Username("admin"), cancellationToken))
            {
                var user = new User(EntityId.Generate(), "admin", "zaq1@WSX", "first_name", "last_name", "admin@gmail.com");
                Role role = await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == new RoleName("admin"), cancellationToken);
                user.SetRole(role);
                await dbContext.AddAsync(user, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            if (!await dbContext.Users.AnyAsync(x => x.Username == new Username("user"), cancellationToken))
            {
                var user = new User(EntityId.Generate(), "user", "zaq1@WSX", "first_name", "last_name", "user@gmail.com");
                var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == new RoleName("user"), cancellationToken);
                user.SetRole(role);
                await dbContext.AddAsync(user, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        private static async Task AddClaims(CinemaSystemDbContext dbContext, IEnumerable<RoleClaim> claims, CancellationToken cancellationToken)
        {
            if (!await dbContext.RoleClaim.AnyAsync(cancellationToken))
            {
                await dbContext.AddRangeAsync(claims, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        private static IEnumerable<RoleClaim> Claims =>
            new List<RoleClaim>()
            {
                //Adminitator claims
                new RoleClaim(Guid.Parse("00000000-0000-0000-0000-100000000000"),"get_claims"),
                new RoleClaim(Guid.Parse("00000000-0000-0000-0000-200000000000"),"add_role"),
                //User claims
                new RoleClaim(Guid.Parse("00000000-0000-0000-0000-000000000001"),"view_products"),
            };

        #endregion Seeding
    }
}