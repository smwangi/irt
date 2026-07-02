using Irt.Application.Dispatchers;
using Irt.Application.IndicatorMainCategories;
using Irt.Application.IndicatorMainCategories.Commands;
using Irt.SharedKernel.Common;
using IrtWeb.GraphQL;
using Results = Irt.SharedKernel.Results;

namespace IrtWeb.GraphQL.IndicatorMainCategories;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class IndicatorMainCategoryMutations
{
    public async Task<IndicatorMainCategoryDto> CreateIndicatorMainCategory(
        CreateIndicatorMainCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                CreateIndicatorMainCategoryCommand,
                Results.Result<IndicatorMainCategoryDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<IndicatorMainCategoryDto> PatchIndicatorMainCategory(
        PatchIndicatorMainCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                PatchIndicatorMainCategoryCommand,
                Results.Result<IndicatorMainCategoryDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<IndicatorMainCategoryDto> UpdateIndicatorMainCategory(
        UpdateIndicatorMainCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                UpdateIndicatorMainCategoryCommand,
                Results.Result<IndicatorMainCategoryDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<bool> DeleteIndicatorMainCategory(
        DeleteIndicatorMainCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                DeleteIndicatorMainCategoryCommand,
                Results.Result<Unit>>(input, cancellationToken);

        return result.ValueOrThrow() == Unit.Value;
    }
}
