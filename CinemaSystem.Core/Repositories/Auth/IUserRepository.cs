using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;

namespace CinemaSystem.Core.Repositories.Auth
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

        Task<User?> GetByUsernameAsync(Username userName, CancellationToken cancellationToken);

        Task CreateAsync(User newUser, CancellationToken cancellationToken);
    }
}