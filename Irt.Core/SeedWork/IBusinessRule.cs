namespace Irt.Core.SeedWork
{
    public interface IBusinessRule
    {
        Task<bool> IsBrokenAsync(); // Evaluates whether the rule is broken asynchronously

        string Message { get; } // error message for the broken rule
        Task<string> ErrorMessage { get; }
    }
}