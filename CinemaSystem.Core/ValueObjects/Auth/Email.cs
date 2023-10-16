using System.Text.RegularExpressions;
using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record Email
    {
        private static readonly Regex Regex = new(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);

        public const int MinLenght = 3;
        public const int MaxLenght = 20;
        public string Value { get; }
        public Email(string value)
        {
            value = value?.ToLowerInvariant();

            if (string.IsNullOrEmpty(value)
                || value.Length is < MinLenght or > MaxLenght
                || !Regex.IsMatch(value))
            {
                var propName = GetType().Name;
                throw new InvalidTextException(propName, value);
            }

            Value = value;
        }

        public static implicit operator string(Email value) => value.Value;

        public static implicit operator Email(string value) => new(value);

        public override string ToString() => Value;
    }
}