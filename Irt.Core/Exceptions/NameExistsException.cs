namespace Irt.Core.Exceptions
{
    public class NameExistsException : DomainException
    {
        public string Name { get; }
        public NameExistsException(string name)
         : base($"Name already exists: {name}.")
        {
            Name = name;
        }
    }
}