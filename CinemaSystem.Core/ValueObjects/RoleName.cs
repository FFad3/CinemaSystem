using CinemaSystem.Core.Exceptions;

namespace CinemaSystem.Core.ValueObjects
{
    public class RoleName
    {
        public string Value { get; }

        public RoleName(string claimName)
        {
            if (string.IsNullOrEmpty(claimName))
            {
                var propName = this.GetType().Name;
                throw new InvalidTextException(propName, claimName);
            }

            Value = claimName.ToUpper();
        }

        public static implicit operator string(RoleName claim) => claim.Value;

        public static implicit operator RoleName(string claimName) => new(claimName);
    }
}