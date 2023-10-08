using CinemaSystem.Application.Abstraction.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace CinemaSystem.Infrastructure.Security
{
    internal sealed class HttpContextTokenStorage : ITokenStorage
    {
        public const string TokenKey = "jwt";
        public const string RefreshTokenKey = "refresh-token";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string? GetToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[TokenKey];
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(TokenKey, token);
        }

    }
}
