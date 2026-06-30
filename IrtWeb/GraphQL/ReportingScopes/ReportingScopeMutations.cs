using Irt.Application.Dispatchers;
using Irt.Application.ReportingScopes;
using Irt.Application.ReportingScopes.Commands;

namespace IrtWeb.GraphQL.ReportingScopes;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class ReportingScopeMutations
{
    public async Task<ReportingScopeDto> CreateReportingScope(
        CreateReportingScopeCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<CreateReportingScopeCommand, Irt.SharedKernel.Results.Result<ReportingScopeDto>>(input, cancellationToken);
        
        return result.IsSuccess
            ? result.Value!
            : throw new GraphQLException(result.IrtError!.Message);
    }
    
    public async Task<ReportingScopeDto> PatchReportingScope(
        PatchReportingScopeCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<PatchReportingScopeCommand, Irt.SharedKernel.Results.Result<ReportingScopeDto>>(input, cancellationToken);
        
        return result.IsSuccess
            ? result.Value!
            : throw new GraphQLException(result.IrtError!.Message);
    }
    
    public async Task<ReportingScopeDto> UpdateReportingScope(
        UpdateReportingScopeCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<UpdateReportingScopeCommand, Irt.SharedKernel.Results.Result<ReportingScopeDto>>(input, cancellationToken);
        
        return result.IsSuccess
            ? result.Value!
            : throw new GraphQLException(result.IrtError!.Message);
    }
}