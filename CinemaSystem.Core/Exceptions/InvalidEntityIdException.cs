namespace CinemaSystem.Core.Exceptions
{
    public sealed class InvalidEntityIdException : CustomValidationException
    {
        public object Id { get; }

        public InvalidEntityIdException(object id) : base(nameof(Id), id, $"Cannot set: {id}  as entity identifier.") => Id = id;
    }
}