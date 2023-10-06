namespace CinemaSystem.Core.Exceptions
{
    public abstract class CustomValidationException : CustomException
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

        protected CustomValidationException(IReadOnlyDictionary<string, string[]> errors, string message) : base(message)
        {
            Errors = errors;
        }

        protected CustomValidationException(string propertyName, object propertyValue, string message)
            : this(new Dictionary<string, string[]> { { propertyName, new[] { propertyValue?.ToString() } } }, message)
        {
        }
    }
}