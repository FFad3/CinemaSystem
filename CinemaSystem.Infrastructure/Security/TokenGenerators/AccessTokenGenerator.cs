﻿using System.Security.Claims;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Models.Auth;
using CinemaSystem.Core.Entities;
using Microsoft.Extensions.Options;

namespace CinemaSystem.Infrastructure.Security.TokenGenerators
{
    internal sealed class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;
        private readonly IClock _clock;

        public AccessTokenGenerator(IOptions<AuthenticationConfiguration> configuration, TokenGenerator tokenGenerator, IClock clock)
        {
            _configuration = configuration.Value;
            _tokenGenerator = tokenGenerator;
            _clock = clock;
        }

        public TokenDetails GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.RoleName)
            };

            var now = _clock.Current();
            var expirationTime = now.AddMinutes(_configuration.AccessTokenExpirationInMinutes);

            var accesToken = _tokenGenerator.GenerateToken(
                _configuration.AccessTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                now,
                expirationTime,
                claims
                );

            return new TokenDetails(accesToken, expirationTime);
        }
    }
}