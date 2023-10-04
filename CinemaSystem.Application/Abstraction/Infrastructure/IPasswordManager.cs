namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface IPasswordManager
    {
        string Secure(string password);

        bool Validate(string password, string securedPasssword);
    }
}