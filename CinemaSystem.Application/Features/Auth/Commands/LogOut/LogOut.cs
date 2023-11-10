using CinemaSystem.Application.Abstraction.Common.Auth;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Abstraction.Requests;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.LogOut
{
    public sealed record LogOut() : ICommand<Unit>, ISecret;

    internal sealed class LogOutCommandHandler : ICommandHandler<LogOut, Unit>
    {
        private readonly IAuthenticator _authenticator;

        public LogOutCommandHandler(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public async Task<Unit> Handle(LogOut request, CancellationToken cancellationToken)
        {
            await _authenticator.RemoveSession(cancellationToken);

            return Unit.Value;
        }
    }
}