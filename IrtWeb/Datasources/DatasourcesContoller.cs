using Asp.Versioning;
using Irt.Application.Datasource;
using Irt.Application.Datasource.Commands;
using Irt.Application.Datasource.Queries;
using Irt.Application.Dispatchers;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IrtWeb.Datasources
{
    [ApiController]
    [ControllerName("Datasource")]
    [Route("irt/api/v{version:apiVersion}")]
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
            var datasources = await _queryDispatcher
                .DispatchAsync<GetDatasourceQuery, Irt.SharedKernel.Results.Result<IQueryable<DatasourceDto>>>(
                    new GetDatasourceQuery(), CancellationToken.None);
            return datasources.ToActionResult();
        }
        
        [HttpPost(ApiPrefix)]
        public async Task<IActionResult> CreateDatasource([FromBody] CreateDatasourceRequest request)
        {
            var createDatasourceCommand = request.ToCommand();
         
            var resp = await _commandDispatcher
                .DispatchAsync<CreateDatasourceCommand, Irt.SharedKernel.Results.Result<DatasourceDto>>(
                    createDatasourceCommand, cancellationToken: CancellationToken.None);
            
            if (!resp.IsSuccess)
            {
                return resp.ToActionResult();
            }
            return resp.ToActionResult();
        }

        [HttpGet(ApiPrefix + "/{id}")]
        public async Task<IActionResult> GetDatasource([FromRoute]string id)
        {
            var datasource = await _queryDispatcher
                .DispatchAsync<GetDatasourceByIdQuery, Irt.SharedKernel.Results.Result<IQueryable<DatasourceDto>>>(
                    new GetDatasourceByIdQuery(id), CancellationToken.None);
            return datasource.ToActionResult();
        }

        [HttpPut(ApiPrefix+ "/{id}")]
        public async Task<IActionResult> UpdateDatasource([FromRoute]string id, [FromBody] UpdateDatasourceRequest request)
        {
            var command = request.ToCommand(id);
            var resp = await _commandDispatcher
                .DispatchAsync<UpdateDatasourceCommand, Irt.SharedKernel.Results.Result<DatasourceDto>>(
                    command, cancellationToken: CancellationToken.None);
            return resp.ToActionResult();
        }

        [HttpPatch(ApiPrefix+ "/{id}")]
        public async Task<IActionResult> PatchDatasource([FromRoute]string id, [FromBody] UpdateDatasourceRequest request)
        {
            var command = request.ToCommand(id);
            var resp = await _commandDispatcher
                .DispatchAsync<UpdateDatasourceCommand, Irt.SharedKernel.Results.Result<DatasourceDto>>(
                    command, CancellationToken.None);
            return resp.ToActionResult();
        }

        [HttpDelete(ApiPrefix+ "/{id}")]
        public async Task<IActionResult> DeleteDatasource([FromRoute]string id)
        {
            var resp = await _commandDispatcher
                .DispatchAsync<DeleteDatasourceCommand, Irt.SharedKernel.Results.Result<Unit>>(
                    new DeleteDatasourceCommand(id), CancellationToken.None);
            return resp.ToActionResult();
        }
    }
}
