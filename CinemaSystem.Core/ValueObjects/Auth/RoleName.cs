using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects.Auth
{
    public sealed record RoleName
    {
        public const int MinLenght = 4;
        public const int MaxLenght = 20;

        public string Value { get; }

        public RoleName(string roleName)
        {
            if (!IsNameValid(roleName))
            {
                throw new InvalidTextException(roleName);
            }

            Value = roleName.ToUpper();
        }

        private static bool IsNameValid(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is < MinLenght or > MaxLenght)
            {
                return false;
            }
            return true;
        }

        public static implicit operator string(RoleName claim) => claim.Value;

        public static implicit operator RoleName(string claimName) => new(claimName);
    }
}