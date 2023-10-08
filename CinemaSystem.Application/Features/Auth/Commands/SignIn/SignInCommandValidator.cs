using CinemaSystem.Core.ValueObjects;
using FluentValidation;

namespace CinemaSystem.Application.Features.Auth.Commands.SignIn
{
    internal sealed class SignInCommandValidator : AbstractValidator<SignIn>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(Username.MinLenght)
                .MaximumLength(Username.MaxLenght);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(Password.MinLenght)
                .MaximumLength(Password.MaxLenght);
        }
    }
}