using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Application.Exceptions;
using CinemaSystem.Application.Models.Auth;
using CinemaSystem.Core.Repositories.Auth;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.SignIn
{
    internal sealed class SignInCommandHandler : ICommandHandler<SignIn, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IAuthenticator _authenticator;

        public SignInCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager, IAuthenticator authenticator)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _authenticator = authenticator;
        }

        public async Task<Unit> Handle(SignIn request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken) ?? throw new InvalidCredentialsException();

            _ = _passwordManager.Validate(user.Password, request.Password) ? true : throw new InvalidCredentialsException();

            var tokensPair = await _authenticator.Authenticate(user, cancellationToken);

            return Unit.Value;
        }
    }
}