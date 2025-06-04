using System.ComponentModel;
using AutoMapper;
using Irt.Application.Configuration.Queries;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Queries;
using Irt.Application.Helpers;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasource.Queries
{
    internal class GetDatasourceQueryHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : IQueryHandler<GetDatasourceQuery, Result<List<DatasourceDto>>>
    {
        public async Task<Result<List<DatasourceDto>>> HandleAsync(
            GetDatasourceQuery request, 
            CancellationToken cancellationToken)
        {
            return await datasourceRepository
                .CreateFactory<Core.Datasources.Datasource>()
                .GetAllAsync()
                .MapAsync(mapper.Map<List<DatasourceDto>>);
        }
    }

    internal class GetDatasourceByIdQueryHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : IQueryHandler<GetDatasourceByIdQuery, Result<DatasourceDto>>
    {
        public async Task<Result<DatasourceDto>> HandleAsync(
            GetDatasourceByIdQuery request, 
            CancellationToken cancellationToken)
        {
            return await datasourceRepository
                .CreateFactory<Core.Datasources.Datasource>()
                .FindByIdAsync(request.Id)
                .MapAsync(mapper.Map<DatasourceDto>);
        }
    }
}