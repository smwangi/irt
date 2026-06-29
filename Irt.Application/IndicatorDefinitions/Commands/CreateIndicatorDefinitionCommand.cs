using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Commands;

public record CreateIndicatorDefinitionCommand : ICommand<Result<IndicatorDefinitionDto>>, IRequireMetadata
{
    public string Name { get; init; }
    public string Description { get; init; }
    
    public string? UserId { get; private set; }
    public string? UserName { get; private set; }
    public string? Application { get; private set; }
    public string? IpAddress { get; private set; }
    public void SetMetadata(string userId, string userName, string application, string ipAddress)
    {
        UserId = userId;
        UserName = userName;
        Application = application;
        IpAddress = ipAddress;
    }
}