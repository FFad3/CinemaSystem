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
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordManager _passwordHasher;

        public SignUpCommandHandler(IUserRepository userRepository, IPasswordManager passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(SignUp request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Secure(request.Password);

            var userRole = await _roleRepository.GetByNameAsync("user", cancellationToken);

            var newUser = new User(EntityId.Generate(), request.Username, hashedPassword, request.FirstName, request.LastName, request.Email, userRole.Id);

            await _userRepository.CreateAsync(newUser, cancellationToken);

            return Unit.Value;
        }
    }
}