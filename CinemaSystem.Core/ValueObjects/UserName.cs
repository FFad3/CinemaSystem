using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects
{
    public sealed record UserName
    {
        public const int MinLenght = 3;
        public const int MaxLenght = 20;
        public string Value { get; }
        public UserName(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is < MinLenght or > MaxLenght)
            {
                var propName = this.GetType().Name;
                throw new InvalidTextException(propName, value);
            }

            Value = value;
        }

        public static implicit operator string(UserName value) => value.Value;

        public static implicit operator UserName(string value) => new(value);

        public override string ToString() => Value;
    }
}