using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategory : NamedMetadataEntity<IndicatorMainCategoryId>
    {
        private IndicatorMainCategory() { }

        private IndicatorMainCategory(
            IndicatorMainCategoryId id,
            Name indicatorMainCategoryName,
            string description)
            : base(id, indicatorMainCategoryName, description)
        {
        }

        public static IndicatorMainCategory Create(
            Name name,
            string description)
        {
            return new IndicatorMainCategory(
                IndicatorMainCategoryId.Create(UniqueIdGenerator.NextId()),
                name,
                description);
        }
    }
}
