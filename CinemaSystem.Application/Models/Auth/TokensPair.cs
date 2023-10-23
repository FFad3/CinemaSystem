namespace CinemaSystem.Application.Models.Auth
{
    public class TokensPair
    {
        public TokenDetails AccessTokenDetails { get; private set; }
        public TokenDetails RefreshTokenDetails { get; private set; }

        public TokensPair(TokenDetails token, TokenDetails refreshToken)
        {
            AccessTokenDetails = token;
            RefreshTokenDetails = refreshToken;
        }
    }
}