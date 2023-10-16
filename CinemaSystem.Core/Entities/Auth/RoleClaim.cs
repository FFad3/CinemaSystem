using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;

namespace CinemaSystem.Core.Entities
{
    public class RoleClaim : IEquatable<RoleClaim>
    {
        public EntityId Id { get; private set; }
        public ClaimName ClaimName { get; private set; }
        public ICollection<Role> Roles { get; private set; } = new List<Role>();

        public RoleClaim(EntityId id, ClaimName claimName)
        {
            this.Id = id;
            this.ClaimName = claimName;
        }

        public void ChangeName(ClaimName claimName)
        {
            this.ClaimName = claimName;
        }

        public override bool Equals(object obj)
        {
            if (obj is RoleClaim claim)
                return Equals(claim);

            return false;
        }

        public bool Equals(RoleClaim other)
        {
            return this.Id.Equals(other.Id) && this.ClaimName.Equals(other.ClaimName);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}