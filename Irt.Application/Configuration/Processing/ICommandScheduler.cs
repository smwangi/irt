using System.Threading.Tasks;
using Irt.Application.Configuration.Commands;

namespace Irt.Application.Configuration.Processing
{
    public interface ICommandScheduler
    {
        Task EnqueueAsync<T>(T command) where T : ICommand;
    }
}