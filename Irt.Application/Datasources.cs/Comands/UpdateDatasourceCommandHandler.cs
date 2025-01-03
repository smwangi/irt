using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Results;
using Irt.Application.Exceptions;
using Irt.Core.Datasources;
using Irt.Core.SharedKernel;

namespace Irt.Application.Datasources.Commands.Handlers
{
    public class UpdateDatasourceCommandHandler : ICommandHandler<UpdateDatasourceCommand, UpdateResult<string>>
    {
        private readonly IDatasourceRepository _datasourceRepository;
        private readonly INameValidationChecker<Datasource> _datasourceUniqueChecker;

        public UpdateDatasourceCommandHandler(IDatasourceRepository datasourceRepository, INameValidationChecker<Datasource> datasourceUniqueChecker)
        {
            _datasourceRepository = datasourceRepository;
            _datasourceUniqueChecker = datasourceUniqueChecker;
        }

        public async Task<UpdateResult<string>> Handle(UpdateDatasourceCommand request, CancellationToken cancellationToken)
        {
            var datasource = await _datasourceRepository.GetByIdAsync(new DatasourceId(request.DatasourceRequest.Id), cancellationToken) ?? throw new NotFoundException(request.DatasourceRequest.Id);

            // update datasource with new values
            datasource.UpdateDatasource(
                request.DatasourceRequest.Name,
                request.DatasourceRequest.Description,
                new DatasourceId(request.Id),
                _datasourceUniqueChecker);

            await _datasourceRepository.UpdateAsync(datasource, cancellationToken);
            return UpdateResult<string>.Success("Update Successful");
        }
    }
}