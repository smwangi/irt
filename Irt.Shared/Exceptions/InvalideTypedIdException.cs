namespace Irt.Shared.Exceptions
{
    public class InvalideTypedIdException : DomainException
    {
        public override string Code => "invalid_typed_id";  

        public string Id { get; }
        public InvalideTypedIdException(string id) 
            : base($"Invalid type ID: '{id}'. Id value cannot be null or empty.") => Id = id;
    }
}