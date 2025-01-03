using Irt.Core.SeedWork;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategoryId : TypedIdValueBase
    {
        public IndicatorMainCategoryId(string value) : base(value)
        {
        }

        public static implicit operator IndicatorMainCategoryId(string id)
            => new(id);
    }
}