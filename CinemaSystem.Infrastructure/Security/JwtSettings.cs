namespace CinemaSystem.Infrastructure.Security
{
    internal sealed class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}