using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorCategories
{
    public class IndicatorCategoryId(string value) : TypedIdValueBase(value)
    {
        public string Value { get; } = value;
    }
}