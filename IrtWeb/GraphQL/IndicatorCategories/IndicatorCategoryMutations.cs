using Irt.Application.Dispatchers;
using Irt.Application.IndicatorCategories;
using Irt.Application.IndicatorCategories.Commands;
using Irt.SharedKernel.Common;
using IrtWeb.GraphQL;
using Results = Irt.SharedKernel.Results;

namespace IrtWeb.GraphQL.IndicatorCategories;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class IndicatorCategoryMutations
{
    public async Task<IndicatorCategoryDto> CreateIndicatorCategory(
        CreateIndicatorCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                CreateIndicatorCategoryCommand,
                Results.Result<IndicatorCategoryDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<IndicatorCategoryDto> PatchIndicatorCategory(
        PatchIndicatorCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                PatchIndicatorCategoryCommand,
                Results.Result<IndicatorCategoryDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<IndicatorCategoryDto> UpdateIndicatorCategory(
        UpdateIndicatorCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                UpdateIndicatorCategoryCommand,
                Results.Result<IndicatorCategoryDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<bool> DeleteIndicatorCategory(
        DeleteIndicatorCategoryCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                DeleteIndicatorCategoryCommand,
                Results.Result<Unit>>(input, cancellationToken);

        return result.ValueOrThrow() == Unit.Value;
    }
}
