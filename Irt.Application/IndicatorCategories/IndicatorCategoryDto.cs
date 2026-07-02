namespace Irt.Application.IndicatorCategories;

public sealed class IndicatorCategoryDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string IndicatorMainCategoryId { get; init; } = string.Empty;
    public string IndicatorMainCategoryName { get; init; } = string.Empty;
}
