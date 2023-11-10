using System.IdentityModel.Tokens.Jwt;

namespace CinemaSystem.Application.Models.Auth
{
    public sealed record Token
    {
        public string Value { get; }

        public Token(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }
            Value = token;
        }

        public JwtSecurityToken Decode()
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(Value);
        }

        public static implicit operator Token(string token) => new(token);

        public static implicit operator string(Token token) => token.Value;
    }
}