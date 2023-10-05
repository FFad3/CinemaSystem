using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Infrastructure.Exceptions
{
    public class FluentValidationException : CustomValidationException
    {
        public FluentValidationException(IReadOnlyDictionary<string, string[]> errors) : base(errors, "Validation error")
        {
        }
    }
}
