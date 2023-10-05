namespace CinemaSystem.Core.Exceptions
{
    public sealed class InvalidTextException : CustomValidationException
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }

        public InvalidTextException(string propertyName, string value) : base(propertyName, value, $"{propertyName}: '{value}' is invalid.")
        {
            PropertyName = propertyName;
            Value = value;
        }
    }
}