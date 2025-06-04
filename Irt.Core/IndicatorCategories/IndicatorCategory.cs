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
        
        private IndicatorCategory() { }

        private IndicatorCategory(
            IndicatorCategoryId id,
            Name name,
            string description,
            IndicatorMainCategory indicatorMainCategory)
        {
            Id = id;
            Name = name;
            Description = description;
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
    }
}