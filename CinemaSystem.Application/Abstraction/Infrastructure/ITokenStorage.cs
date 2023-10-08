namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface ITokenStorage
    {
        void SetToken(string token);
        string? GetToken();
    }
}