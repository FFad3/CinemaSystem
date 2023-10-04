using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories;
using CinemaSystem.Core.ValueObjects;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    internal sealed class SignUpCommandHandler : ICommandHandler<SignUp, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordHasher;

        public SignUpCommandHandler(IUserRepository userRepository, IPasswordManager passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(SignUp request, CancellationToken cancellationToken)
        {
            var passwordHash = _passwordHasher.Secure(request.Password);

            var Username = new UserName(request.Username);
            var HashPassword = new Password(passwordHash);
            var Email = new Email(request.Email);
            var FirstName = new FirstName(request.FirstName);
            var LastName = new LastName(request.LastName);
            var UserId = Guid.NewGuid();

            var newUser = new User(UserId, Username, HashPassword, FirstName, LastName, Email);

            var createdUser = await _userRepository.CreateAsync(newUser, cancellationToken);

            return createdUser;
        }
    }
}