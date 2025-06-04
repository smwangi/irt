using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategory : Entity<IndicatorMainCategoryId>
    {
        public string Description { get; private set; }
        
        private IndicatorMainCategory() { }

        private IndicatorMainCategory(
            IndicatorMainCategoryId id,
            Name indicatorMainCategoryName,
            string description)
        {
            Id = id;
            Name = indicatorMainCategoryName;
            Description = description;
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