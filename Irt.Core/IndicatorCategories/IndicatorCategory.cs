using Irt.Core.IndicatorMainCategories;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.IndicatorCategories
{
    public class IndicatorCategory : Entity<IndicatorCategoryId>
    {
        public string Description { get; private set; }
        public IndicatorMainCategory IndicatorMainCategory { get; private set; }

        private IndicatorCategory(
            IndicatorCategoryId id,
            string description,
            IndicatorMainCategory indicatorMainCategory) : base(id)
        {
            Id = id;
            Description = description;
            IndicatorMainCategory = indicatorMainCategory;
        }

        public static IndicatorCategory CreateIndicatorCategory(
            string description,
            IndicatorMainCategory indicatorMainCategory)
        {
            return new IndicatorCategory(
                new IndicatorCategoryId(description),
                description,
                indicatorMainCategory);
        }
    }
}