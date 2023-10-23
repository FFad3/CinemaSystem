namespace CinemaSystem.Application.Models.Auth
{
    public sealed record TokenDetails
    {
        public Token Token { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public TokenDetails(Token token, DateTime expirationDate)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            Token = token;
            ExpirationDate = expirationDate;
        }

        public void Update(Token token,DateTime expirationDate)
        {
            Token=token;
            ExpirationDate=expirationDate;
        }
    }
}