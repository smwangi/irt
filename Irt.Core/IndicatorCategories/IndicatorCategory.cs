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
            IndicatorMainCategory indicatorMainCategory)
        {
            Id = id;
            Description = description;
            IndicatorMainCategory = indicatorMainCategory;
        }

        public static IndicatorCategory CreateIndicatorCategory(
            Name name,
            string description,
            IndicatorMainCategory indicatorMainCategory,
            INameValidationChecker<IndicatorCategory> nameValidationChecker)
        {
            CheckRule(new NameUniquenessChecker<IndicatorCategory>(nameValidationChecker, name, null));
            return new IndicatorCategory(
                new IndicatorCategoryId(description),
                description,
                indicatorMainCategory);
        }
    }
}