using MongoDB.Bson;

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
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        protected InternalCommandBase(string id)
        {
            this.Id = id;
        }
    }
}