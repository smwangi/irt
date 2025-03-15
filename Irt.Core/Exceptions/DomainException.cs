using System.Runtime.Serialization;

namespace Irt.Core.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}