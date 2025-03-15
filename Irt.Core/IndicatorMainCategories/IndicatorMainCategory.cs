using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategory : Entity<IndicatorMainCategoryId>
    {
        public string Description { get; private set; }

        private IndicatorMainCategory(
            IndicatorMainCategoryId id,
            Name indicatorMainCategoryName,
            string description) : base(id)
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
                new IndicatorMainCategoryId(
                    UniqueIdGenerator.NextId()),
                name,
                description);
        }
    }
}