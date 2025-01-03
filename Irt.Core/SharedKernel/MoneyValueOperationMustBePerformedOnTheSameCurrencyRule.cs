using Irt.Core.SeedWork;

namespace Irt.Core.SharedKernel
{
    
    public class MoneyValueOperationMustBePerformedOnTheSameCurrencyRule : IBusinessRule
    {
        private readonly MoneyValue _valueLeft;
        private readonly MoneyValue _valueRight;

        public MoneyValueOperationMustBePerformedOnTheSameCurrencyRule(MoneyValue left, MoneyValue right)
        {
            _valueLeft = left;
            _valueRight = right;
        }

        public string Message => "Money value operation must be performed on the same currency.";

        public Task<bool> IsBroken() => Task.FromResult(_valueLeft.Currency != _valueRight.Currency);
    }
}