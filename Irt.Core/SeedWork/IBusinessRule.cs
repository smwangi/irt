namespace Irt.Core.SeedWork
{
    public interface IBusinessRule
    {
        Task<bool> IsBroken();

        string Message { get; }
    }
}