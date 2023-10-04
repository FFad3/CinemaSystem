using CinemaSystem.Application.Abstraction.Infrastructure;

namespace CinemaSystem.Infrastructure.Time
{
    internal class Clock : IClock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}