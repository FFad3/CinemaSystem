using CinemaSystem.Application.Models.Auth;

namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface ITokenStorage
    {
        void SetTokens(TokensPair tokensPair);
        void SetAccesToken(Token token);
        void SetRefreshToken(Token refreshToken);
        Token? GetAccessToken();
        Token? GetRefreshToken();
    }
}