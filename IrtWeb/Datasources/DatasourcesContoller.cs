using Asp.Versioning;
using Irt.Application.Configuration.Notifications;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IrtWeb.Datasources
{
    [ApiController]
    [ControllerName("Datasources")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DatasourcesController : ODataController
    {
        private const string ApiPrefix = "datasources";
        private readonly IMediator _mediator;
        public DatasourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetDatasources()
        {
            var datasources = await _mediator.Send(new GetDatasourcesQuery());
            return Ok(datasources);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDatasource([FromBody] DatasourceDto request)
        {
            var resp = await _mediator.Send(
                new CreateDatasourceCommand(
                    request.Name,
                    request.Description,
                    request.Source,
                    request.DatasourceType));
            // Trigger notification
            await _mediator.Publish(new DatasourceAddedNotification());
            return Ok(resp);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetDatasource([FromRoute]string id)
        {
            var datasource = await _mediator.Send(new GetDatasourcesByIdQuery(id));
            return Ok(datasource);
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateDatasource([FromRoute]string id, [FromBody] DatasourceDto request)
        {
            var resp = await _mediator.Send(new UpdateDatasourceCommand(request));
            return Ok(resp);
        }

        [HttpPatch("/{id}")]
        public async Task<IActionResult> PatchDatasource([FromRoute]string id, [FromBody] DatasourceDto request)
        {
            var resp = await _mediator.Send(new UpdateDatasourceCommand(request));
            return Ok(resp);
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteDatasource([FromRoute]string id)
        {
            var resp = await _mediator.Send(new DeleteDatasourceCommand(id));
            return Ok(resp);
        }
    }
}