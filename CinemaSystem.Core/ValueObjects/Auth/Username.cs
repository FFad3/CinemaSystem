using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record Username
    {
        public const int MinLenght = 3;
        public const int MaxLenght = 20;
        public string Value { get; }
        public Username(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length is < MinLenght or > MaxLenght)
            {
                throw new InvalidTextException(username);
            }

            Value = username;
        }

        public static implicit operator string(Username value) => value.Value;

        public static implicit operator Username(string value) => new(value);

        public override string ToString() => Value;
    }
}