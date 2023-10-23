using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace CinemaSystem.Infrastructure.Security
{
    internal sealed class HttpContextTokenStorage : ITokenStorage
    {
        public const string AccessTokenKey = "access-token";
        public const string RefreshTokenKey = "refresh-token";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Token? GetAccessToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[AccessTokenKey];
        }
        public Token? GetRefreshToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[RefreshTokenKey];
        }
        public void SetRefreshToken(Token refreshToken)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(RefreshTokenKey, refreshToken);
        }

        public void SetAccesToken(Token token)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(AccessTokenKey, token);
        }

        public void SetTokens(TokensPair tokensPair)
        {
            SetAccesToken(tokensPair.AccessTokenDetails.Token);
            SetRefreshToken(tokensPair.RefreshTokenDetails.Token);
        }
    }
}