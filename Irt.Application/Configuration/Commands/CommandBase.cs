// Code Created: 2020-07-19 1:00 PM
using MongoDB.Bson;

namespace Irt.Application.Configuration.Commands
{
    public abstract class CommandBase : ICommand
    {
        public string Id { get; }

        public CommandBase()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        protected CommandBase(string id)
        {
            this.Id = id;
        }
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        public string Id { get; }

        protected CommandBase()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        protected CommandBase(string id)
        {
            this.Id = id;
        }
    }
}