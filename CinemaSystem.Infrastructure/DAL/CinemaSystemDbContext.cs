using CinemaSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Infrastructure.DAL
{
    internal class CinemaSystemDbContext : DbContext
    {
        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion
        public CinemaSystemDbContext(DbContextOptions<CinemaSystemDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
