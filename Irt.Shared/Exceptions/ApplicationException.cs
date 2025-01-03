namespace Irt.Shared.Exceptions
{
    public abstract class ApplicationException(string message) : Exception(message)
    {
        public virtual string? Code { get; }
    }
}