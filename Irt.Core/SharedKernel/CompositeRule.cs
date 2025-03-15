using Irt.Core.SeedWork;

namespace Irt.Core.SharedKernel;

public class CompositeRule(IEnumerable<IBusinessRule> rules) : IBusinessRule
{
    private readonly List<Task<string>> _brokenRuleMessages = new();
    public async Task<bool> IsBrokenAsync()
    {
        _brokenRuleMessages.Clear();
        foreach (var rule in rules)
        {
            if (await rule.IsBrokenAsync())
            {
                _brokenRuleMessages.Add(rule.ErrorMessage);
            }
        }
        return _brokenRuleMessages.Any();
    }

    public string Message { get; }

    //public string Message => string.Join(",", rules.Where(r => r.IsBroken()).Select(r => r.Message));
    public Task<string> ErrorMessage => Task.FromResult(string.Join("; ", _brokenRuleMessages));
}