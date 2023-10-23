using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Models.Auth;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Infrastructure.Security.TokenGenerators;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CinemaSystem.Infrastructure.Security
{
    internal class Authenticator : IAuthenticator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IClock _clock;
        private readonly ITokenStorage _tokenStorage;
        private readonly ICacheService _cacheService;
        private readonly IUserRepository _userRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
            IClock clock, ITokenStorage tokenStorage, ICacheService cacheService,
            IOptions<AuthenticationConfiguration> configuration, IUserRepository userRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _clock = clock;
            _tokenStorage = tokenStorage;
            _cacheService = cacheService;
            _configuration = configuration.Value;
            _userRepository = userRepository;
        }

        public async Task<TokensPair> Authenticate(User user, CancellationToken cancellationToken)
        {
            var tokenPair = CreateNewTokenPair(user);

            await UpdateCache(tokenPair,cancellationToken);

            _tokenStorage.SetTokens(tokenPair);

            return tokenPair;
        }

        public async Task<TokensPair> RefreshSession(CancellationToken cancellationToken)
        {
            //TO DO: Add token validators;
            var accessToken = _tokenStorage.GetAccessToken();
            var refreshToken = _tokenStorage.GetRefreshToken();

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(accessToken))
            {
                throw new SecurityTokenException("invalid tokens pair");
            }

            var cachedToken = await _cacheService.GetAsync<Token>(refreshToken, cancellationToken) ?? throw new SecurityTokenException("invalid tokens pair");

            if (cachedToken != accessToken)
            {
                throw new SecurityTokenException("invalid tokens pair");
            }

            var decodedToken = DecodeToken(cachedToken);
            var userEmailAddressClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            var user = await _userRepository.GetByEmailAsync(userEmailAddressClaim.Value, cancellationToken);

            var tokenPair = CreateNewTokenPair(user);

            await UpdateCache(tokenPair, cancellationToken);

            _tokenStorage.SetTokens(tokenPair);

            return tokenPair;
        }

        private static JwtSecurityToken DecodeToken(Token token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }

        private async Task UpdateCache(TokensPair tokensPair, CancellationToken cancellationToken)
        {
            string cacheKey = tokensPair.RefreshTokenDetails.Token;
            string cacheValue = tokensPair.AccessTokenDetails.Token;
            await _cacheService.SetAsync(cacheKey, cacheValue, cancellationToken);
        }

        private TokensPair CreateNewTokenPair(User user)
        {
            var currentTime = _clock.Current();

            var accessTokenDetails = _accessTokenGenerator.GenerateToken(user, currentTime);

            var refreshTokenDetails = _refreshTokenGenerator.GenerateToken(currentTime);

            return new TokensPair(accessTokenDetails, refreshTokenDetails);
        }
    }
}