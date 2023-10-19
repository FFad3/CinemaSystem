using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories.Auth;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Infrastructure.DAL.Repositories.Auth
{
    internal sealed class ClaimRepository : IClaimRepository
    {
        private readonly DbSet<RoleClaim> _roles;

        public ClaimRepository(CinemaSystemDbContext context)
        {
            _roles = context.RoleClaim;
        }

        public async Task<IEnumerable<RoleClaim>> GetAllAsync(CancellationToken cancellationToken) =>
            await _roles.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}