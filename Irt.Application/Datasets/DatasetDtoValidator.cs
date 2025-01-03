namespace Irt.Application.Datasets;

using FluentValidation;

public class DatasetDtoValidator : AbstractValidator<DatasetDto>
{
    public DatasetDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Matches("^[a-zA-Z0-9-]+$").WithMessage("Id must be alphanumeric.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
    }
}