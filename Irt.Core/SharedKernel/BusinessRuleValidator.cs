// Fluent API for validation of business rules

using Irt.Core.SeedWork;
using BusinessRuleValidationException = Irt.Core.Datasources.BusinessRuleValidationException;

namespace Irt.Core.SharedKernel;

public class BusinessRuleValidator
{
    private readonly List<IBusinessRule> _rules = new();

    public BusinessRuleValidator AddRule(IBusinessRule rule)
    {
        _rules.Add(rule);
        return this;
    }

    public async Task Validate()
    {
        foreach (var rule in _rules)
        {
            if (await rule.IsBrokenAsync())
            {
                throw new BusinessRuleValidationException(rule.Message);
            }
        }
    }
}