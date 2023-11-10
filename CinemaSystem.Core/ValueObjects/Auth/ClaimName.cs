using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record ClaimName
    {
        public const int MinLenght = 4;
        public const int MaxLenght = 20;

        public string Value { get; }

        public ClaimName(string claimName)
        {
            if (!IsNameValid(claimName))
            {
                throw new InvalidTextException(claimName);
            }

            Value = claimName.ToUpper();
        }

        private static bool IsNameValid(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is < MinLenght or > MaxLenght)
            {
                return false;
            }

            return true;
        }

        public static implicit operator string(ClaimName claim) => claim.Value;

        public static implicit operator ClaimName(string claimName) => new(claimName);
    }
}