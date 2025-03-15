
namespace Irt.Application.Configuration.Commands
{
    public interface ICommand<TResult>
    {
        string Id { get; }
    }
    
    // For commands without result
    public interface ICommand {}
}