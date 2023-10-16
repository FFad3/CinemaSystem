using CinemaSystem.Application.Extensions;
using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Core.ValueObjects.Auth;
using FluentValidation;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    public sealed class SignUpCommandValidator : AbstractValidator<SignUp>
    {
        private readonly IUserRepository _userRepository;

        public SignUpCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Username)
                .ValidateUsername(_userRepository);

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
                .ValidateEmail(_userRepository);
        }
    }
}