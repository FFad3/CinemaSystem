using FluentValidation;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    internal class SignUpCommandValidator : AbstractValidator<SignUp>
    {
        public SignUpCommandValidator()
        {
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