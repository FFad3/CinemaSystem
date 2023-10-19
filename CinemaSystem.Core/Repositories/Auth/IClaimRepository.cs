using CinemaSystem.Core.Entities;

namespace CinemaSystem.Core.Repositories.Auth
{
    public interface IClaimRepository
    {
        Task<IEnumerable<RoleClaim>> GetAllAsync(CancellationToken cancellationToken);
    }
}