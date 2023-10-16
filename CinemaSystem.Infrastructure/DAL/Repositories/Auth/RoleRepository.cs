using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Infrastructure.DAL.Repositories.Auth
{
    internal sealed class RoleRepository : IRoleRepository
    {
        private readonly DbSet<Role> _roles;

        public RoleRepository(CinemaSystemDbContext context)
        {
            _roles = context.Roles;
        }

        public async Task CreateAsync(Role newRole, CancellationToken cancellationToken) =>
            await _roles.AddAsync(newRole, cancellationToken);

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken)=>
            await _roles.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Role?> GetByIdAsync(EntityId id, CancellationToken cancellationToken) =>
            await _roles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Role?> GetByNameAsync(RoleName roleName, CancellationToken cancellationToken) =>
            await _roles.SingleOrDefaultAsync(x => x.RoleName == roleName, cancellationToken);
    }
}