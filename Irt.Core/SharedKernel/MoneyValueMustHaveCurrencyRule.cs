using Irt.Core.SeedWork;

namespace Irt.Core.SharedKernel
{
    public class MoneyValueMustHaveCurrencyRule(string currency) : IBusinessRule
    {
        public string Message => "Money value must have currency specified.";
        public Task<string> ErrorMessage => Task.FromResult($"Money value must have currency specified");

        public bool IsBroken()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBrokenAsync() => Task.FromResult(string.IsNullOrWhiteSpace(currency));
    }
}