using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using MongoDB.Bson;

namespace Irt.Core.IndicatorMainCategories
{
    public class IndicatorMainCategory : Entity<IndicatorMainCategoryId>
    {
        public string Description { get; private set; }

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
            string description,
            INameValidationChecker<IndicatorMainCategory> nameValidationChecker)
        {
            CheckRule(new NameUniquenessChecker<IndicatorMainCategory>(nameValidationChecker, name, null));
            return new IndicatorMainCategory(new IndicatorMainCategoryId(ObjectId.GenerateNewId().ToString()), name, description);
        }
    }
}