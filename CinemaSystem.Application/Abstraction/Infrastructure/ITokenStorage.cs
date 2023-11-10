using CinemaSystem.Application.Models.Auth;

namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface ITokenStorage
    {
        void SetTokens(TokensPair tokensPair);
        void SetToken(string cookieName, TokenDetails tokenDetails);
        void ClearTokens();
        Token? GetAccessToken();
        Token? GetRefreshToken();
    }
}