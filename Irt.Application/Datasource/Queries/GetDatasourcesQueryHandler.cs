using System.ComponentModel;
using AutoMapper;
using Irt.Application.Configuration.Queries;
using Irt.Application.Helpers;
using Irt.Core.Datasources;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasources.Queries
{
    internal class GetDatasourceQueryHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : IQueryHandler<GetDatasourcesQuery, Result<List<DatasourceDto>>>
    {
        public async Task<Result<List<DatasourceDto>>> HandleAsync(
            GetDatasourcesQuery request, 
            CancellationToken cancellationToken)
        {
            return await datasourceRepository
                .CreateFactory<Datasource>()
                .GetAllAsync()
                .MapAsync(mapper.Map<List<DatasourceDto>>);
        }
    }

    internal class GetDatasourceByIdQueryHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : IQueryHandler<GetDatasourcesByIdQuery, Result<DatasourceDto>>
    {
        public async Task<Result<DatasourceDto>> HandleAsync(
            GetDatasourcesByIdQuery request, 
            CancellationToken cancellationToken)
        {
            return await datasourceRepository
                .CreateFactory<Datasource>()
                .FindByIdAsync(request.Id)
                .MapAsync(mapper.Map<Result<DatasourceDto>>);
        }
    }
}