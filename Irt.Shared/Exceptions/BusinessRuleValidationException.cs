using Irt.Core.SeedWork;

namespace Irt.Shared.Exceptions
{
    public class BusinessRuleValidationException : DomainException
    {   
        public IBusinessRule BusinessRule { get; }

        public BusinessRuleValidationException(IBusinessRule businessRule) : base(businessRule.Message)
        {
            BusinessRule = businessRule;
        }
    }
}