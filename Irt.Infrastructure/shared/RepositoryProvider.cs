using Irt.Application.Helpers;
using Irt.Core.SharedKernel;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Infrastructure.Shared;

// Service Locator
public class RepositoryProvider(IServiceProvider serviceProvider) : IRepositoryProvider
{

    public IRepository<T> GetRepository<T>() where T : class
    {
        return serviceProvider.GetRequiredService<IRepository<T>>();
    }
}