using Asp.Versioning;
using Irt.Application.Configuration.Results;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Commands;
using Irt.Application.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IrtWeb.Datasources
{
    [ApiController]
    [ControllerName("Datasources")]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class DatasourcesController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher) : ODataController
    {
        private const string ApiPrefix = "datasources";

        private readonly ICommandDispatcher _commandDispatcher =
            commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        private readonly IQueryDispatcher _queryDispatcher = 
            queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        
        [HttpGet(ApiPrefix)]
        [EnableQuery]
        public async Task<IActionResult> GetDatasources()
        {
            var datasources = await _queryDispatcher.DispatchAsync<GetDatasourcesQuery, Result<List<DatasourceDto>, string>>(new GetDatasourcesQuery(), CancellationToken.None);
            return Ok(datasources);
        }
        
        [HttpPost(ApiPrefix)]
        public async Task<IActionResult> CreateDatasource([FromBody] DatasourceDto datasourceDto)
        {
            var createDatasourceCommand = new CreateDatasourceCommand(datasourceDto);
         
            var resp = await _commandDispatcher.DispatchAsync<CreateDatasourceCommand, Result<DatasourceDto, string>>(
                createDatasourceCommand, cancellationToken: CancellationToken.None);
            return Ok(resp);
        }

        [HttpGet(ApiPrefix + "/{{id}}")]
        public async Task<IActionResult> GetDatasource([FromRoute]string id)
        {
            var datasource = await _queryDispatcher.DispatchAsync<GetDatasourcesByIdQuery, DatasourceDto>(new GetDatasourcesByIdQuery(id), CancellationToken.None);
            return Ok(datasource);
        }

        [HttpPut(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> UpdateDatasource([FromRoute]string id, [FromBody] DatasourceDto request)
        {
            if (request.Id != id)
            {
                throw new ArgumentException("Id mismatch");
            }
            
            var command = new UpdateDatasourceCommand(request);
            var resp = await _commandDispatcher.DispatchAsync<UpdateDatasourceCommand, Result<DatasourceDto, string>>(command, cancellationToken: CancellationToken.None);
            return Ok(resp);
        }

        [HttpPatch(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> PatchDatasource([FromRoute]string id, [FromBody] DatasourceDto request)
        {
            if (request.Id != id)
            {
                throw new ArgumentException("Id mismatch");
            }
            var resp = await _commandDispatcher.DispatchAsync<UpdateDatasourceCommand, Result<DatasourceDto, string>>(new UpdateDatasourceCommand(request), CancellationToken.None);
            return Ok(resp);
        }

        [HttpDelete(ApiPrefix+ "/{{id}}")]
        public async Task<IActionResult> DeleteDatasource([FromRoute]string id)
        {
            var resp = await _commandDispatcher.DispatchAsync<DeleteDatasourceCommand, DeleteDatasourceResult>(new DeleteDatasourceCommand(id), CancellationToken.None);
            return Ok(resp);
        }
    }
}