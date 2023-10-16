using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Core.ValueObjects.Common;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    internal sealed class SignUpCommandHandler : ICommandHandler<SignUp, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordHasher;

        public SignUpCommandHandler(IUserRepository userRepository, IPasswordManager passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(SignUp request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Secure(request.Password);

            var newUser = new User(EntityId.Generate(), request.Username, hashedPassword, request.FirstName, request.LastName, request.Email);

            await _userRepository.CreateAsync(newUser, cancellationToken);

            return Unit.Value;
        }
    }
}