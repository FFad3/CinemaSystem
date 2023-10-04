using CinemaSystem.Core.ValueObjects;

namespace CinemaSystem.Core.Entities
{
    public class User
    {
        //TO DO: how to set to auto generate by db
        public EntityId Id { get; }

        public UserName Username { get; }
        public Password Password { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public Email Email { get; }

        public User(EntityId id, UserName username, Password password, FirstName firstName, LastName lastName, Email email)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}