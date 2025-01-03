using Irt.Core.Exceptions;
using Irt.Core.SeedWork;

namespace Irt.Core.Datasources
{
    public class BusinessRuleValidationException(IBusinessRule businessRule) : DomainException(businessRule.Message)
    {
        public IBusinessRule BusinessRule { get; } = businessRule;
    }
}