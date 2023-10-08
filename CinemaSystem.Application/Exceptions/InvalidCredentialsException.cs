using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Application.Exceptions
{
    public sealed class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException() : base("Invalid credentials")
        {
        }

        private InvalidCredentialsException(string message) : base(message)
        {
        }
    }
}
