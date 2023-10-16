using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record Password
    {
        public const int MinLenght = 6;
        public const int MaxLenght = 200;
        public string Value { get; }
        public Password(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is < MinLenght or > MaxLenght)
            {
                var propName = GetType().Name;
                throw new InvalidTextException(propName, value);
            }
            Value = value;
        }

        public static implicit operator string(Password value) => value.Value;

        public static implicit operator Password(string value) => new(value);

        public override string ToString() => Value;
    }
}