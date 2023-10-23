using CinemaSystem.Application.Models.Auth;
using CinemaSystem.Core.Entities;

namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface IAuthenticator
    {
        public Task<TokensPair> Authenticate(User user, CancellationToken cancellationToken);
        public Task<TokensPair> RefreshSession(CancellationToken cancellationToken);
    }
}
