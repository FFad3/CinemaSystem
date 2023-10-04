using CinemaSystem.Core.Entities;

namespace CinemaSystem.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken);

        Task<User> CreateAsync(User newUser, CancellationToken cancellationToken);
    }
}