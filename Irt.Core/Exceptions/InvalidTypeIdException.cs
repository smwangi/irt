namespace Irt.Core.Exceptions
{
    public class InvalidTypeIdException : DomainException
    {
        public string Id { get; }
        public InvalidTypeIdException(string id) 
            : base($"Invalid type id: {id}. Id must be a non-empty string.")
        {
            Id = id;
        }
    }
}