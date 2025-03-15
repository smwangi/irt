using System.Linq.Expressions;
using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;
using BusinessRuleValidationException = Irt.Core.Datasources.BusinessRuleValidationException;

namespace Irt.Core.SharedKernel;

public class NameMustBeUniqueRule<T>(Name name, IRepository<T> repository, Expression<Func<T, Name>> nameSelector)
    : IBusinessRule
    where T : class
{

    public async Task<bool> IsBrokenAsync()
    {
        return await repository.ExistsAsync(entity => nameSelector.Compile()(entity) == name);
    }
    
    public string Message => "Name must be unique.";
    public Task<string> ErrorMessage => Task.FromResult($"Name must be unique");

    public async Task Check()
    {
        if (await IsBrokenAsync())
        {
            throw new BusinessRuleValidationException(
                Message,
                nameof(NameMustBeUniqueRule<T>),
                $"The name '{name}' is already in use in entity {typeof(T).Name}.");
        }
    }
}