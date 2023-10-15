using CinemaSystem.Core.ValueObjects;

namespace CinemaSystem.Core.Entities
{
    public class Role
    {
        public EntityId Id { get; private set; } = EntityId.Generate();
        public RoleName RoleName { get; private set; }
        public virtual ICollection<RoleClaim> Claims { get; private set; } = new List<RoleClaim>();

        public Role(EntityId id,RoleName roleName, ICollection<RoleClaim>? claims)
        {
            this.Id = id;
            this.RoleName = roleName;
            this.Claims = claims;
        }

        public void ChangeName(RoleName roleName)
        {
            this.RoleName = roleName;
        }

        public void AddClaim(RoleClaim claim)
        {
            if (Claims.Contains(claim))
                throw new InvalidOperationException();
            Claims.Add(claim);
        }

        public void RemoveClaim(RoleClaim claim)
        {
            Claims.Remove(claim);
        }
    }
}