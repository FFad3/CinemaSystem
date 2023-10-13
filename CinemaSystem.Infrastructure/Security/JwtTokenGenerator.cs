using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CinemaSystem.Infrastructure.Security
{
    internal sealed class JwtTokenGenerator : ITokenGenarator
    {
        private readonly IClock _clock;
        private readonly JwtSettings _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();

        public JwtTokenGenerator(IOptions<JwtSettings> options, IClock clock)
        {
            _options = options.Value;
            _clock = clock;
        }

        public static SigningCredentials CreateSigningCredentials(SecurityKey SecurityKey) =>
            new(SecurityKey, SecurityAlgorithms.HmacSha256);

        public static SecurityKey CreateSecurityKey(string SigningKey) =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey));

        public string GenarateToken(User user, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,Role)
            };

            var now = _clock.Current();
            var expires = now.Add(TimeSpan.FromHours(_options.Expiry));

            var securityKey = CreateSecurityKey(_options.SigningKey);
            var signingCredentials = CreateSigningCredentials(securityKey);

            var token = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: expires,
                    signingCredentials: signingCredentials
                );

            return _jwtSecurityToken.WriteToken(token);
        }
    }
}