using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects
{
    public sealed record ClaimName
    {
        public string Value { get; }

        public ClaimName(string claimName)
        {
            if (string.IsNullOrEmpty(claimName))
            {
                var propName = this.GetType().Name;
                throw new InvalidTextException(propName, claimName);
            }

            Value = claimName.ToUpper();
        }

        public static implicit operator string(ClaimName claim) => claim.Value;

        public static implicit operator ClaimName(string claimName) => new(claimName);
    }
}