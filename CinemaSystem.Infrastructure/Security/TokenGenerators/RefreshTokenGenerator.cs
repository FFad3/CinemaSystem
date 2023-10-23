using CinemaSystem.Application.Models.Auth;
using Microsoft.Extensions.Options;

namespace CinemaSystem.Infrastructure.Security.TokenGenerators
{
    internal sealed class RefreshTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public RefreshTokenGenerator(IOptions<AuthenticationConfiguration> configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration.Value;
            _tokenGenerator = tokenGenerator;
        }

        public TokenDetails GenerateToken(DateTime now)
        {
            var expirationTime = now.AddMinutes(_configuration.RefreshTokenExpirationInMinutes);

            var accesToken = _tokenGenerator.GenerateToken(
                _configuration.RefreshTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                now,
                expirationTime
                );

            return new TokenDetails(accesToken, expirationTime);
        }
    }
}