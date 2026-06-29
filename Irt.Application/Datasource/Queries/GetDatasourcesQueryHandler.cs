using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Extensions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using CoreDatasource = Irt.Core.Datasources.Datasource;

namespace Irt.Application.Datasource.Queries
{
    internal class GetDatasourceQueryHandler(
        IReadOnlyRepository<Core.Datasources.Datasource> datasourceRepository,
        IProjection<CoreDatasource, DatasourceDto> projection)
        : IODataQueryHandler<GetDatasourceQuery, Result<IQueryable<DatasourceDto>>>
    {
        public Task<Result<IQueryable<DatasourceDto>>> HandleAsync(GetDatasourceQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(
                datasourceRepository
                    .QuerySafely(projection.Expression)
                    .Bind(q => q.ApplyODataSafely(request.Options)));
        }
    }

    internal class GetDatasourceByIdQueryHandler(
        IReadOnlyRepository<Core.Datasources.Datasource> datasourceRepository,
        IProjection<CoreDatasource, DatasourceDto> projection)
        : IQueryHandler<GetDatasourceByIdQuery, Result<IQueryable<DatasourceDto>>>
    {
        public Task<Result<IQueryable<DatasourceDto>>> HandleAsync(GetDatasourceByIdQuery request,
            CancellationToken cancellationToken)
        {
            var projectedQuery = datasourceRepository
                .QueryById(request.Id, projection.Expression);
            return Task.FromResult(Result<IQueryable<DatasourceDto>>.Success(projectedQuery));
        }
    }
}
