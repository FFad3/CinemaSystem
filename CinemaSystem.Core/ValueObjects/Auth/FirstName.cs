using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record FirstName
    {
        public const int MinLenght = 3;
        public const int MaxLenght = 20;
        public string Value { get; }
        public FirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName) || firstName.Length is < MinLenght or > MaxLenght)
            {
                throw new InvalidTextException(firstName);
            }

            Value = firstName;
        }

        public static implicit operator string(FirstName value) => value.Value;

        public static implicit operator FirstName(string value) => new(value);

        public override string ToString() => Value;
    }
}