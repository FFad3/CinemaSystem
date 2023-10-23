using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemaSystem.Application.Models.Auth;
using Microsoft.IdentityModel.Tokens;

namespace CinemaSystem.Infrastructure.Security.TokenGenerators
{
    internal class TokenGenerator
    {
        private static SigningCredentials CreateSigningCredentials(SecurityKey SecurityKey) =>
            new(SecurityKey, SecurityAlgorithms.HmacSha256);

        private static SecurityKey CreateSecurityKey(string signingKey) =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

        public Token GenerateToken(string signingKey, string issuer, string audience, DateTime notBefore, DateTime utcExpirationTime,
            IEnumerable<Claim> claims = null)
        {
            SecurityKey securityKey = CreateSecurityKey(signingKey);
            SigningCredentials signingCredentials = CreateSigningCredentials(securityKey);

            JwtSecurityToken token = new(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: notBefore,
                expires: utcExpirationTime,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}