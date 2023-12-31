﻿using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CinemaSystem.Infrastructure.Security
{
    public class PasswordManager : IPasswordManager
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordManager(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Secure(string password) => _passwordHasher.HashPassword(default!, password);

        public bool Validate(string hashedPassword, string password) =>
            _passwordHasher.VerifyHashedPassword(default!, hashedPassword, password) == PasswordVerificationResult.Success;
    }
}