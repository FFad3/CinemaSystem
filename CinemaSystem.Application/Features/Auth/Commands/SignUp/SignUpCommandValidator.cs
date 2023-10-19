using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Extensions;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using FluentValidation;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    internal sealed class SignUpCommandValidator : AbstractValidator<SignUp>
    {
        public SignUpCommandValidator(IGenericRepository<User> userRepository)
        {
            RuleFor(x => x.Username)
                .ValidateUsername(userRepository);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(Password.MinLenght)
                .MaximumLength(Password.MaxLenght)
                ;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(FirstName.MinLenght)
                .MaximumLength(FirstName.MaxLenght)
                ;

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(LastName.MinLenght)
                .MaximumLength(LastName.MaxLenght)
                ;

            RuleFor(x => x.Email)
                .ValidateEmail(userRepository);
        }
    }
}