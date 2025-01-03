using MediatR;

namespace Irt.Application.Configuration.Commands
{
    public interface ICommand : IRequest
    {
        string Id { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        string Id { get; }
    }
}