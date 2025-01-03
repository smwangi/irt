using System.ComponentModel;
using Irt.Core.Datasources;
using MediatR;

namespace Irt.Application.Datasources.Queries
{
    internal class GetDatasourcesQueryHandler(IDatasourceRepository datasourceRepository) : IRequestHandler<GetDatasourcesQuery, List<DatasourceDto>>
    {
        private readonly IDatasourceRepository _datasourceRepository = datasourceRepository;

        public async Task<List<DatasourceDto>> Handle(GetDatasourcesQuery request, CancellationToken cancellationToken)
        {
            var datasources = await _datasourceRepository.GetAllAsync(cancellationToken);
            return datasources.Select(d => new DatasourceDto
            (
                Id: d.Id.Value, // Add null check and provide a default value
                Name: d.Name.Value, // Add null check and provide a default value
                Description: d.Description ?? string.Empty, // Add null check and provide a default value
                Source: d.Source ?? string.Empty, // Add null check and provide a default value
                DatasourceType: d.DatasourceType.ToString(), // Add null check and provide a default value
                CreatedBy: d.CreatedBy, // Add null check and provide a default value
                CreatedAt: d.CreatedAt, // Add null check and provide a default value
                LastModifiedBy: d.LastModifiedBy, // Add null check and provide a default value
                LastModifiedAt: d.LastModifiedAt // Add null check and provide a default value
            )).ToList();
        }
    }

    internal class GetDatasourcesByIdQueryHandler(IDatasourceRepository datasourceRepository) : IRequestHandler<GetDatasourcesByIdQuery, DatasourceDto>
    {
        private readonly IDatasourceRepository _datasourceRepository = datasourceRepository;

        public async Task<DatasourceDto> Handle(GetDatasourcesByIdQuery request, CancellationToken cancellationToken)
        {
            var datasource = await _datasourceRepository.GetByIdAsync(new DatasourceId(request.Id), cancellationToken) ?? throw new Exception($"Datasource not found {request.Id}");
            return new DatasourceDto
            (
                Id: datasource.Id.Value.ToString(),
                Name: datasource.Name.Value,
                Description: datasource.Description ?? string.Empty, // Add null check and provide a default value
                Source: datasource.Source ?? string.Empty, // Add null check and provide a default value
                DatasourceType: datasource.DatasourceType.ToString(), // Add null check and provide a default value
                CreatedBy: datasource.CreatedBy, // Add null check and provide a default value
                CreatedAt: datasource.CreatedAt, // Add null check and provide a default value
                LastModifiedBy: datasource.LastModifiedBy, // Add null check and provide a default value
                LastModifiedAt: datasource.LastModifiedAt // Add null check and provide a default value
            );
        }
    }
}