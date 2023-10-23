namespace CinemaSystem.Infrastructure.Security
{
    internal sealed class AuthenticationConfiguration
    {
        public const string SectionName = "AuthenticationConfiguration";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AccessTokenSecret { get; set; }
        public double AccessTokenExpirationInMinutes { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double RefreshTokenExpirationInMinutes { get; set; }
    }
}