namespace Irt.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
            InnerExceptionDetails = message;
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
            InnerExceptionDetails = innerException.Message;
        }

        public BadRequestException(string name, object key) : base($"{name} ({key}) was not found.")
        {
            InnerExceptionDetails = $"{name} ({key}) was not found.";
        }

        public string InnerExceptionDetails { get; } = default!;
    }
}