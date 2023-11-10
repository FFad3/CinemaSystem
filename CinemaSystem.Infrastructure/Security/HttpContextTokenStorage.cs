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
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(AccessTokenKey, out string? accessToken);
            return accessToken;
        }

        public Token? GetRefreshToken()
        {
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(RefreshTokenKey, out string? refreshToken);
            return refreshToken;
        }

        public void SetToken(string cookieName, TokenDetails tokenDetails)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = tokenDetails.ExpirationDate
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, tokenDetails.Token, options);
        }

        public void SetTokens(TokensPair tokensPair)
        {
            SetToken(AccessTokenKey, tokensPair.AccessTokenDetails);
            SetToken(RefreshTokenKey, tokensPair.RefreshTokenDetails);
        }

        public void ClearTokens()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(AccessTokenKey);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(RefreshTokenKey);
        }
    }
}