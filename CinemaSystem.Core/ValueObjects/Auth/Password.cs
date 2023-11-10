using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record Password
    {
        public const int MinLenght = 6;
        public const int MaxLenght = 200;
        public string Value { get; }
        public Password(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length is < MinLenght or > MaxLenght)
            {
                throw new InvalidTextException(password);
            }
            Value = password;
        }

        public static implicit operator string(Password value) => value.Value;

        public static implicit operator Password(string value) => new(value);

        public override string ToString() => Value;
    }
}