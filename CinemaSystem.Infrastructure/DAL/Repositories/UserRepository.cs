using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories;
using CinemaSystem.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Infrastructure.DAL
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(CinemaSystemDbContext context)
        {
            _users = context.Users;
        }

        public async Task CreateAsync(User newUser, CancellationToken cancellationToken) =>
            await _users.AddAsync(newUser, cancellationToken);

        public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken) =>
            await _users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

        public async Task<User> GetByUserNameAsync(Username userName, CancellationToken cancellationToken) =>
            await _users.SingleOrDefaultAsync(x => x.Username == userName, cancellationToken);
    }
}