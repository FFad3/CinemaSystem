using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Models.Auth;
using Microsoft.Extensions.Options;

namespace CinemaSystem.Infrastructure.Security.TokenGenerators
{
    internal sealed class RefreshTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;
        private readonly IClock _clock;

        public RefreshTokenGenerator(IOptions<AuthenticationConfiguration> configuration, TokenGenerator tokenGenerator, IClock clock)
        {
            _configuration = configuration.Value;
            _tokenGenerator = tokenGenerator;
            _clock = clock;
        }

        public TokenDetails GenerateToken()
        {
            var now = _clock.Current();
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