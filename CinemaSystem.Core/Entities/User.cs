using CinemaSystem.Core.ValueObjects;

namespace CinemaSystem.Core.Entities
{
    public class User
    {
        public EntityId Id { get; private set; }
        public Username Username { get; private set; }
        public Password Password { get; private set; }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }

        public User(EntityId id, Username username, Password password, FirstName firstName, LastName lastName, Email email)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }
    }
}