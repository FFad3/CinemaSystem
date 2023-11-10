using System.Runtime.CompilerServices;

namespace CinemaSystem.Core.Exceptions
{
    public sealed class InvalidTextException : CustomValidationException
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }

        public InvalidTextException(string value, [CallerArgumentExpression("value")] string propertyName = null) : base(propertyName, value, $"{propertyName}: '{value}' is invalid.")
        {
            PropertyName = propertyName;
            Value = value;
        }
    }
}