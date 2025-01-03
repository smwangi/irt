using System;
using Irt.Core.SeedWork;

namespace Irt.Core.SharedKernel
{
    public class MoneyValue : ValueObject
    {
        public MoneyValue(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }
        public string Currency { get; }

        public static MoneyValue Of(decimal amount, string currency) => new(amount, currency);
        public static MoneyValue Of(MoneyValue moneyValue) => new(moneyValue.Amount, moneyValue.Currency);

        public static MoneyValue operator *(decimal factor, MoneyValue value) => new MoneyValue(factor * value.Amount, value.Currency);

        public static MoneyValue operator *(int factor, MoneyValue value) => new MoneyValue(factor * value.Amount, value.Currency);

        public static MoneyValue operator +(MoneyValue valueLeft, MoneyValue valueRight)
        {
            CheckRule(new MoneyValueOperationMustBePerformedOnTheSameCurrencyRule(valueLeft, valueRight));

            return new MoneyValue(valueLeft.Amount + valueRight.Amount, valueLeft.Currency);
        }
    }

    public static class SumExtensions
    {
        public static MoneyValue Sum(this MoneyValue valueLeft, MoneyValue valueRight) => valueLeft + valueRight;

        public static MoneyValue Sum<T>(this IEnumerable<MoneyValue> source) => source.Aggregate((x, y) => x + y);

        public static MoneyValue Sum<T>(this IEnumerable<T> source, Func<T, MoneyValue> selector) => MoneyValue.Of(source.Select(selector).Aggregate((x, y) => x + y));
    }
}