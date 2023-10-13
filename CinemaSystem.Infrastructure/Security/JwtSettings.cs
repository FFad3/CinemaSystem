namespace CinemaSystem.Infrastructure.Security
{
    internal sealed class JwtSettings
    {
        public const string SectionName = "JWT";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int Expiry { get; set; }
    }
}