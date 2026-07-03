namespace Irt.Application.IndicatorCategories.Commands;

public interface IIndicatorCategoryUpsertCommand
{
    string Name { get; }
    string Description { get; }
    string IndicatorMainCategoryId { get; }
}
