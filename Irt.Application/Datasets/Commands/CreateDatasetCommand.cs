
using FluentValidation;
using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.Datasets.Commands;

public record CreateDatasetCommand(
    string Name,
    string Description,
    string DatasourceId,
    string DatasetType,
    string IndicatorDefinitionId) : ICommand<Result<DatasetDto>>, IRequireMetadata
{
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


public class CreateDatasetCommandValidator : AbstractValidator<CreateDatasetCommand>
{
    public CreateDatasetCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.DatasourceId).NotEmpty().WithMessage("DatasourceId is required.");
        RuleFor(x => x.IndicatorDefinitionId).NotEmpty().WithMessage("IndicatorDefinitionId is required.");
        RuleFor(x => x.DatasetType).NotEmpty().WithMessage("DatasetType is required.");
    }
}