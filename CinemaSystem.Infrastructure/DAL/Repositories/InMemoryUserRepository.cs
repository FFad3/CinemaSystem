using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories;
using CinemaSystem.Core.ValueObjects;

namespace CinemaSystem.Infrastructure.DAL
{
    internal sealed class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public InMemoryUserRepository()
        {
        }

        public Task CreateAsync(User newUser, CancellationToken cancellationToken)
        {
            _users.Add(newUser);
            return Task.CompletedTask;
        }

        public Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            var result = _users.SingleOrDefault(x => x.Email == email);
            return Task.FromResult(result);
        }

        public Task<User> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken)
        {
            var result = _users.SingleOrDefault(x => x.Username == userName);
            return Task.FromResult(result);
        }
    }
}