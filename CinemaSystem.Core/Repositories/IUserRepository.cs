﻿using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects;

namespace CinemaSystem.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken);

        Task<User> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken);

        Task CreateAsync(User newUser, CancellationToken cancellationToken);
    }
}