using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;

namespace CinemaSystem.Core.Entities
{
    public class Role
    {
        public EntityId Id { get; private set; }
        public RoleName RoleName { get; private set; }
        public ICollection<RoleClaim> Claims { get; private set; }  = new List<RoleClaim>();

        public Role(EntityId id, RoleName roleName)
        {
            this.Id = id;
            this.RoleName = roleName;
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