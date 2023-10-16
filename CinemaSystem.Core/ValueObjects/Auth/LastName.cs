using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record LastName
    {
        public const int MinLenght = 3;
        public const int MaxLenght = 20;
        public string Value { get; }
        public LastName(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is < MinLenght or > MaxLenght)
            {
                var propName = GetType().Name;
                throw new InvalidTextException(propName, value);
            }

            Value = value;
        }

        public static implicit operator string(LastName value) => value.Value;

        public static implicit operator LastName(string value) => new(value);

        public override string ToString() => Value;
    }
}