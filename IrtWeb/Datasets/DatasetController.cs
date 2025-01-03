using Asp.Versioning;
using Irt.Application.Configuration.Notifications;
using Irt.Application.Datasets;
using Irt.Application.Datasets.Commands;
using Irt.Application.Datasets.Queries;
using Irt.Application.Configuration.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IrtWeb.Datasets
{
    [ApiController]
    [ControllerName("Datasets")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DatasetsController : ODataController
    {
        ILogger<DatasetsController> _logger;
        private const string ApiPrefix = "datasets";
        private readonly IMediator _mediator;
        public DatasetsController(IMediator mediator, ILogger<DatasetsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetDatasets()
        {
            var datasets = await _mediator.Send(new GetDatasetsQuery());
            return Ok(datasets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDataset([FromBody] DatasetDto request)
        {
            var resp = await _mediator.Send(
                new CreateDatasetCommand(
                    request.Name,
                    request.Description,
                    request.DatasourceId,
                    request.IndicatorDefinitionId,
                    DatasetType.Internal.ToString()));  
            // Trigger notification
            await _mediator.Publish(new DatasetAddedNotification());
            return Ok(resp);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetDataset([FromRoute]string id)
        {
            var dataset = await _mediator.Send(new GetDatasetsByIdQuery(id));
            if (dataset == null)
            {
                return NotFound(new { message = "Dataset not found" });
            }
            return Ok(dataset);
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateDataset([FromRoute]string id, [FromBody] DatasetDto request)
        {
            if (id != request.Id)
            {
                _logger.LogWarning("ID mismatch: Route ID = {RouteId}, Request ID = {RequestId}", id, request.Id);
                return BadRequest(Result<object, string>.Fail("Id Missmatch"));
            }

            /*try
            {
                var resp = await _mediator.Send(new UpdateDatasetCommand(request));
                if (resp == null)
                {
                    return NotFound(new { message = "Dataset not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error updating dataset with ID {Id}", id);
                return StatusCode(500, new { status = "error", message = ex.Message });
            }*/
            _logger.LogInformation("Updating dataset with ID {DatasetId}", id);
            var resp = await _mediator.Send(new UpdateDatasetCommand(request));
            if (!resp.IsSuccess)
            {
                _logger.LogWarning("Update not successful!");
                return resp.Message switch
                {
                    "Dataset not found" => NotFound(Result<string, object>.Fail(resp.Message)),
                    _ => BadRequest(Result<object, string>.Fail(resp.Message ?? "Unknown error"))
                };
            }
            /*return resp.IsSuccess
                ? Ok(Result<string, object>.Ok(resp.Message))
                : NotFound(Result<string, object>.Fail("Dataset not found"));*/
            _logger.LogInformation("Update Successful!");
            return Ok(Result<string, object>.Ok(resp?.Message ?? "Update successful"));
            
        }

        [HttpPatch("/{id}")]
        public async Task<IActionResult> PatchDataset([FromRoute]string id, [FromBody] DatasetDto request)
        {
            if (id != request.Id)
            {
                return BadRequest("Id in the request body does not match the id in the route");
            }
            var resp = await _mediator.Send(new UpdateDatasetCommand(request));
            return Ok(resp);
        }
    }
}