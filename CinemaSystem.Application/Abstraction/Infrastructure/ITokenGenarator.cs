using CinemaSystem.Core.Entities;

namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface ITokenGenarator
    {
        string GenarateToken(User user, string Role);
    }
}