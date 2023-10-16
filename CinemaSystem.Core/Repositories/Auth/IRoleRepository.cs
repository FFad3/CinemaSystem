using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;

namespace CinemaSystem.Core.Repositories.Auth
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(EntityId id, CancellationToken cancellationToken);

        Task<Role?> GetByNameAsync(RoleName roleName, CancellationToken cancellationToken);

        Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken);

        Task CreateAsync(Role newRole, CancellationToken cancellationToken);

    }
}