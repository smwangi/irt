using System.ComponentModel;
using AutoMapper;
using Irt.Application.Configuration.Queries;
using Irt.Application.Configuration.Results;
using Irt.Application.Helpers;
using Irt.Core.Datasources;

namespace Irt.Application.Datasources.Queries
{
    internal class GetDatasourcesQueryHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : IQueryHandler<GetDatasourcesQuery, Result<List<DatasourceDto>, string>>
    {
        private readonly IRepositoryFactory _datasourceRepository = datasourceRepository ?? throw new ArgumentNullException(nameof(datasourceRepository));

        public async Task<Result<List<DatasourceDto>, string>> HandleAsync(GetDatasourcesQuery request, CancellationToken cancellationToken)
        {
            var datasources = await _datasourceRepository.CreateFactory<Datasource>().GetAllAsync();
            return Result<List<DatasourceDto>, string>.Success(mapper.Map<List<DatasourceDto>>(datasources));
        }
    }

    internal class GetDatasourcesByIdQueryHandler(
        IRepositoryFactory datasourceRepository,
        IMapper mapper) : IQueryHandler<GetDatasourcesByIdQuery, DatasourceDto>
    {
        private readonly IRepositoryFactory _datasourceRepository = datasourceRepository ?? throw new ArgumentNullException(nameof(datasourceRepository));

        public async Task<DatasourceDto> HandleAsync(GetDatasourcesByIdQuery request, CancellationToken cancellationToken)
        {
            var datasource = await _datasourceRepository.CreateFactory<Datasource>().FindByIdAsync(request.Id) ?? throw new Exception($"Datasource not found {request.Id}");
            return mapper.Map<DatasourceDto>(datasource);
        }
    }
}