using Irt.Core.IndicatorMainCategories;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.IndicatorCategories
{
    public class IndicatorCategory : NamedMetadataEntity<IndicatorCategoryId>
    {
        public IndicatorMainCategory IndicatorMainCategory { get; private set; }
        
        private IndicatorCategory() { }

        private IndicatorCategory(
            IndicatorCategoryId id,
            Name name,
            string description,
            IndicatorMainCategory indicatorMainCategory)
            : base(id, name, description)
        {
            IndicatorMainCategory = indicatorMainCategory;
        }

        public static IndicatorCategory CreateIndicatorCategory(
            string description,
            Name name,
            IndicatorMainCategory indicatorMainCategory)
        {
            return new IndicatorCategory(
                IndicatorCategoryId.Create(UniqueIdGenerator.NextId()),
                name, 
                description,
                indicatorMainCategory);
        }

        public void Update(
            string name,
            string description,
            IndicatorMainCategory indicatorMainCategory)
        {
            ArgumentNullException.ThrowIfNull(indicatorMainCategory);

            base.Update(name, description);
            IndicatorMainCategory = indicatorMainCategory;
        }
    }
}
