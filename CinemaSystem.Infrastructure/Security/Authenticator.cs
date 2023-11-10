using System.Security.Claims;
using System.Threading;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Models.Auth;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Infrastructure.Security.TokenGenerators;
using Microsoft.IdentityModel.Tokens;

namespace CinemaSystem.Infrastructure.Security
{
    internal class Authenticator : IAuthenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenStorage _tokenStorage;
        private readonly ICacheService _cacheService;
        private readonly IUserRepository _userRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
            ITokenStorage tokenStorage, ICacheService cacheService, IUserRepository userRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenStorage = tokenStorage;
            _cacheService = cacheService;
            _userRepository = userRepository;
        }

        public async Task<TokensPair> Authenticate(User user, CancellationToken cancellationToken)
        {
            var tokenPair = CreateNewTokenPair(user);

            await UpdateCachedTokens(tokenPair, cancellationToken);

            _tokenStorage.SetTokens(tokenPair);

            return tokenPair;
        }

        public async Task<TokensPair> RefreshSession(CancellationToken cancellationToken)
        {
            var cachedToken = await GetCachedToken(cancellationToken);

            var decodedToken = cachedToken.Decode();
            var userEmailAddressClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            var user = await _userRepository.GetByEmailAsync(userEmailAddressClaim.Value, cancellationToken);

            var tokenPair = CreateNewTokenPair(user);

            await UpdateCachedTokens(tokenPair, cancellationToken);

            _tokenStorage.SetTokens(tokenPair);

            return tokenPair;
        }

        private async Task<Token> GetCachedToken(CancellationToken cancellationToken)
        {
            var accessToken = _tokenStorage.GetAccessToken();
            var refreshToken = _tokenStorage.GetRefreshToken();

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(accessToken))
            {
                throw new SecurityTokenException("invalid tokens pair");
            }

            var cachedAccessToken = await _cacheService.GetAsync<Token>(refreshToken, cancellationToken) ?? throw new SecurityTokenException("invalid tokens pair");
            await _cacheService.RemoveAsync(refreshToken, cancellationToken);

            if (cachedAccessToken != accessToken)
            {
                throw new SecurityTokenException("invalid tokens pair");
            }

            return cachedAccessToken;
        }

        private async Task UpdateCachedTokens(TokensPair tokensPair, CancellationToken cancellationToken)
        {
            var expirationDate = tokensPair.RefreshTokenDetails.ExpirationDate;
            string cacheKey = tokensPair.RefreshTokenDetails.Token;
            string cacheValue = tokensPair.AccessTokenDetails.Token;
            await _cacheService.SetAsync(cacheKey, cacheValue, x => x.AbsoluteExpiration = expirationDate, cancellationToken);
        }

        private TokensPair CreateNewTokenPair(User user)
        {
            var accessTokenDetails = _accessTokenGenerator.GenerateToken(user);
            var refreshTokenDetails = _refreshTokenGenerator.GenerateToken();

            return new TokensPair(accessTokenDetails, refreshTokenDetails);
        }

        public async Task RemoveSession(CancellationToken cancellationToken = default)
        {
            var refreshToken = _tokenStorage.GetRefreshToken();

            await _cacheService.RemoveAsync(refreshToken, cancellationToken);

            _tokenStorage.ClearTokens();
        }
    }
}