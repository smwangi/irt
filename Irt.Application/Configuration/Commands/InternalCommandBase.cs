// Code Created: 2021-01-07 3:00 PM

using Irt.Core.SharedKernel;

namespace Irt.Application.Configuration.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        public string Id { get; }

        protected InternalCommandBase(string id)
        {
            this.Id = id;
        }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        public string Id { get; }

        protected InternalCommandBase()
        {
            this.Id = UniqueIdGenerator.NextId();
        }

        protected InternalCommandBase(string id)
        {
            this.Id = id;
        }
    }
}