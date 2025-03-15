using System.Runtime.Serialization;
using Irt.Core.Exceptions;
using Irt.Core.SeedWork;

namespace Irt.Core.Datasources
{
    public class BusinessRuleValidationException(string errorMessage) : DomainException(errorMessage)
    {
        public string RuleName { get; }
        public string Details { get; }

        public BusinessRuleValidationException(string message, string ruleName = null, string details = null): this(message)
        {
            RuleName = ruleName;
            Details = details;
        }
    }
}