using CinemaSystem.Core.Repositories;
using FluentValidation;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    public sealed class SignUpCommandValidator : AbstractValidator<SignUp>
    {
        private readonly IUserRepository _userRepository;
        public SignUpCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}