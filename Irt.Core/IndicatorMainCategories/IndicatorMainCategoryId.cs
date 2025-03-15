

using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategoryId(string value) : TypedIdValueBase<IndicatorMainCategoryId>(value)
    {
        public static implicit operator IndicatorMainCategoryId(string id)
            => new(id);
    }
}