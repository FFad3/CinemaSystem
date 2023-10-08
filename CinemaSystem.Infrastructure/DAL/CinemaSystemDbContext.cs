using CinemaSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Infrastructure.DAL
{
    internal class CinemaSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public CinemaSystemDbContext(DbContextOptions<CinemaSystemDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
