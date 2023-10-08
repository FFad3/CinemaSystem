using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Application.Exceptions;
using CinemaSystem.Core.Repositories;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.SignIn
{
    internal sealed class SignInCommandHandler : ICommandHandler<SignIn, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenGenarator _tokenGenarator;
        private readonly ITokenStorage _tokenStorage;

        public SignInCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager, ITokenGenarator tokenGenarator, ITokenStorage tokenStorage)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _tokenGenarator = tokenGenarator;
            _tokenStorage = tokenStorage;
        }

        public async Task<Unit> Handle(SignIn request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken) ?? throw new InvalidCredentialsException();

            _ = _passwordManager.Validate(request.Password, user.Password) ? true : throw new InvalidCredentialsException();

            var token = _tokenGenarator.GenarateToken(user, "user");

            _tokenStorage.SetToken(token);

            return Unit.Value;
        }
    }
}