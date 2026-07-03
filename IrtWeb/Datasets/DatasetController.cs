using Asp.Versioning;
using Irt.Application.Datasets;
using Irt.Application.Datasets.Commands;
using Irt.Application.Datasets.Queries;
using Irt.Application.Dispatchers;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IrtWeb.Datasets
{
    [ApiController]
    [ControllerName("Datasets")]
    [Route("irt/api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class DatasetsController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        ILogger<DatasetsController> logger) : ODataController
    {
        private const string ApiPrefix = "datasets";
        private readonly ICommandDispatcher _commandDispatcher = commandDispatcher
                                                                 ?? throw new ArgumentNullException(nameof(commandDispatcher));
        private readonly IQueryDispatcher _queryDispatcher = queryDispatcher
                                                             ?? throw new ArgumentNullException(nameof(queryDispatcher));

        [HttpGet(ApiPrefix)]
        [EnableQuery]
        public async Task<IActionResult> GetDatasets()
        {
            var datasets = await _queryDispatcher
                .DispatchAsync<GetDatasetsQuery, Irt.SharedKernel.Results.Result<IQueryable<DatasetDto>>>(
                    new GetDatasetsQuery(), CancellationToken.None);
            
            return datasets.ToActionResult();
        }

        [HttpPost(ApiPrefix)]
        public async Task<IActionResult> CreateDataset([FromBody] CreateDatasetRequest request)
        {
            var command = request.ToCommand();
            var resp = await _commandDispatcher
                .DispatchAsync<CreateDatasetCommand, Irt.SharedKernel.Results.Result<DatasetDto>>(
                    command, CancellationToken.None);
            return resp.ToActionResult();
        }

        [HttpGet(ApiPrefix+ "/{id}")]
        public async Task<IActionResult> GetDataset([FromRoute]string id)
        {
            var dataset = await _queryDispatcher
                .DispatchAsync<GetDatasetsByIdQuery, Irt.SharedKernel.Results.Result<IQueryable<DatasetDto>>>(
                    new GetDatasetsByIdQuery(id), CancellationToken.None);
            
            return dataset.ToActionResult();
        }

        [HttpPut(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> UpdateDataset(
            [FromRoute]string id,
            [FromBody] UpdateDatasetRequest request)
        {
            logger.LogInformation("Updating dataset with ID {DatasetId}", id);
            var command = request.ToCommand(id);
            var resp = await _commandDispatcher
                .DispatchAsync<UpdateDatasetCommand, Irt.SharedKernel.Results.Result<DatasetDto>>(
                    command, CancellationToken.None);
            if (!resp.IsSuccess)
            {
                logger.LogWarning("Update not successful!");
                return resp.ToActionResult();
            }
            logger.LogInformation("Update Successful!");
            return resp.ToActionResult();
            
        }

        [HttpPatch(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> PatchDataset([FromRoute]string id, [FromBody] UpdateDatasetRequest request)
        {
            var command = request.ToCommand(id);
            var resp = await _commandDispatcher
                .DispatchAsync<UpdateDatasetCommand, Irt.SharedKernel.Results.Result<DatasetDto>>(
                    command, CancellationToken.None);
            return resp.ToActionResult();
        }
    }
}
