
namespace Irt.Application.Configuration.Commands
{
    public interface ICommand<out TResult>
    {
    }
    
    // For commands without result
    public interface ICommand {}
}