using FluentValidation;
using Irt.Application.Configuration.ContextAccessor;
using Irt.Application.Datasource;
using Irt.Core.Datasources;

namespace Irt.Application.Datasources.Validators;

public class DatasourceDtoValidator : AbstractValidator<DatasourceDto>
{

    public DatasourceDtoValidator(IOperationContextAccessor contextAccessor)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.DatasourceType).NotEmpty().Must(BeValidDatasourceType)
            .WithMessage($"DatasourceType is required and must be a valid value of: {string.Join(", ", Enum.GetNames<DatasourceType>())}");
        RuleFor(x => x.Id).NotEmpty().When(_ => contextAccessor.OperationType == "Update")
            .WithMessage("Id is required when creating a new datasource");
        
    }
    
    private bool BeValidDatasourceType(string datasourceType)
    {
        return !string.IsNullOrEmpty(datasourceType) && Enum.TryParse<DatasourceType>(datasourceType, out _);
    }
}