using CinemaSystem.Core.Repositories;
using CinemaSystem.Core.ValueObjects;
using FluentValidation;

namespace CinemaSystem.Application.Extensions
{
    internal static class ValidationExtensions
    {
        #region User
        public static void ValidateUsername<T>(this IRuleBuilder<T, string> ruleBuilder, IUserRepository userRepository)
        {
            ruleBuilder
                .NotEmpty()
                .MinimumLength(Username.MinLenght)
                .MaximumLength(Username.MaxLenght)
                .MustAsync(async (name, cancelationToken) =>
                    await userRepository.GetByUsernameAsync(name, cancelationToken) is null)
                .WithMessage("Username must be unique")
                ;
        }

        public static void ValidateEmail<T>(this IRuleBuilder<T, string> ruleBuilder, IUserRepository userRepository)
        {
            ruleBuilder
                .NotEmpty()
                .MinimumLength(Username.MinLenght)
                .MaximumLength(Username.MaxLenght)
                .EmailAddress()
                .MustAsync(async (email, cancelationToken) =>
                    await userRepository.GetByEmailAsync(email, cancelationToken) is null)
                .WithMessage("Email must be unique")
                ;
        }
        #endregion
    }
}