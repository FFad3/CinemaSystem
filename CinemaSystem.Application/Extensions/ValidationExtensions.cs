using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Features.Auth.Commands.CreateRole;
using CinemaSystem.Application.Features.Auth.Commands.SignUp;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using FluentValidation;

namespace CinemaSystem.Application.Extensions
{
    internal static class ValidationExtensions
    {
        #region User

        public static void ValidateUsername(this IRuleBuilder<SignUp, string> ruleBuilder, IGenericRepository<User> repo)
        {
            ruleBuilder
                .NotEmpty()
                .MinimumLength(Username.MinLenght)
                .MaximumLength(Username.MaxLenght)
                .MustAsync(async (name, cancelationToken) =>
                    await repo.IsUnique(x => x.Username.Equals(name), cancelationToken))
                .WithMessage("Username must be unique")
                ;
        }

        public static void ValidateEmail(this IRuleBuilder<SignUp, string> ruleBuilder, IGenericRepository<User> repo)
        {
            ruleBuilder
                .NotEmpty()
                .MinimumLength(Username.MinLenght)
                .MaximumLength(Username.MaxLenght)
                .EmailAddress()
                .MustAsync(async (email, cancelationToken) =>
                    await repo.IsUnique(e => e.Equals(email), cancelationToken))
                .WithMessage("Email must be unique")
                ;
        }

        #endregion User

        #region Role

        public static void ValidateRoleName(this IRuleBuilder<CreateRoleCommand, string> ruleBuilder, IGenericRepository<Role> repo)
        {
            ruleBuilder
                .NotEmpty()
                .MinimumLength(RoleName.MinLenght)
                .MaximumLength(RoleName.MaxLenght)
                .MustAsync(async (roleName, cancelationToken) =>
                 await repo.IsUnique(x => x.RoleName.Equals(roleName), cancelationToken)
                )
                .WithMessage("Role name must be unique")
                ;
        }

        #endregion Role
    }
}