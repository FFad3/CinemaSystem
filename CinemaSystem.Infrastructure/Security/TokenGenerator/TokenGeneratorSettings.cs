namespace CinemaSystem.Infrastructure.Security.TokenGenerator
{
    internal sealed class TokenGeneratorSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}