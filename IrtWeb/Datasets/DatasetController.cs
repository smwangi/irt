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
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class DatasetsController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        ILogger<DatasetsController> logger) : ODataController
    {
        private const string ApiPrefix = "datasets";
        private readonly ICommandDispatcher _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        private readonly IQueryDispatcher _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));

        [HttpGet(ApiPrefix)]
        [EnableQuery]
        public async Task<IActionResult> GetDatasets()
        {
            var datasets = await _queryDispatcher.DispatchAsync<GetDatasetsQuery, Result<List<DatasetDto>>>(new GetDatasetsQuery(), CancellationToken.None);
            return Ok(datasets);
        }

        [HttpPost(ApiPrefix)]
        public async Task<IActionResult> CreateDataset([FromBody] DatasetDto request)
        {
            var resp = await _commandDispatcher.DispatchAsync<CreateDatasetCommand, Result<DatasetDto>>(new CreateDatasetCommand(request), CancellationToken.None);
            return Ok(resp);
        }

        [HttpGet(ApiPrefix+ "/{id}")]
        public async Task<IActionResult> GetDataset([FromRoute]string id)
        {
            var dataset = await _queryDispatcher.DispatchAsync<GetDatasetsByIdQuery, Result<DatasetDto>>(new GetDatasetsByIdQuery(id), CancellationToken.None);
            
            return Ok(dataset);
        }

        [HttpPut(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> UpdateDataset([FromRoute]string id, [FromBody] DatasetDto request)
        {
            if (id != request.Id)
            {
                logger.LogWarning("ID mismatch: Route ID = {RouteId}, Request ID = {RequestId}", id, request.Id);
                return BadRequest("ID mismatch: Route ID does not match Request ID");
            }

            logger.LogInformation("Updating dataset with ID {DatasetId}", id);
            var resp = await _commandDispatcher.DispatchAsync<UpdateDatasetCommand, Result<DatasetDto>>(new UpdateDatasetCommand(request), CancellationToken.None);
            if (!resp.IsSuccess)
            {
                logger.LogWarning("Update not successful!");
                return BadRequest(resp);
            }
            logger.LogInformation("Update Successful!");
            return Ok(resp);
            
        }

        [HttpPatch(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> PatchDataset([FromRoute]string id, [FromBody] DatasetDto request)
        {
            if (id != request.Id)
            {
                return BadRequest("Id in the request body does not match the id in the route");
            }
            var resp = await _commandDispatcher.DispatchAsync<UpdateDatasetCommand, Result<DatasetDto>>(new UpdateDatasetCommand(request), CancellationToken.None);
            return Ok(resp);
        }
    }
}