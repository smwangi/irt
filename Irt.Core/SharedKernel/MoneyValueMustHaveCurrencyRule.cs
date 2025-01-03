using Irt.Core.SeedWork;

namespace Irt.Core.SharedKernel
{
    public class MoneyValueMustHaveCurrencyRule : IBusinessRule
    {
        private readonly string _currency;

        public MoneyValueMustHaveCurrencyRule(string currency)
        {
            _currency = currency;
        }

        public string Message => "Money value must have currency specified.";

        public Task<bool> IsBroken() => Task.FromResult(string.IsNullOrWhiteSpace(_currency));
    }
}