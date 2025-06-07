using AutoMapper;
using Irt.Application.Configuration.Queries;
using Irt.Core.ReportingScopes;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Queries.Handlers;

public class GetReportingScopeQueryHandler(
    IRepositoryProvider repositoryProvider,
    IMapper mapper) : IQueryHandler<GetReportingScopeQuery, Result<List<ReportingScopeDto>>>
{
    public async Task<Result<List<ReportingScopeDto>>> HandleAsync(GetReportingScopeQuery query, CancellationToken cancellationToken)
    {
        var reportingScopeRepository = repositoryProvider.GetRepository<ReportingScope>();
        return await reportingScopeRepository
            .GetAllAsync()
            .MapAsync(mapper.Map<List<ReportingScopeDto>>);
    }
}

public class GetReportingScopeByIdQueryHandler(
    IRepositoryProvider repositoryProvider,
    IMapper mapper) : IQueryHandler<GetReportingScopeByIdQuery, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(GetReportingScopeByIdQuery query, CancellationToken cancellationToken)
    {
        var reportingScopeRepository = repositoryProvider.GetRepository<ReportingScope>();
        return await reportingScopeRepository
            .FindByIdAsync(query.Id)
            .MapAsync(mapper.Map<ReportingScopeDto>);
    }
}