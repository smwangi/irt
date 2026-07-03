using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;

namespace Irt.SharedKernel.Extensions;

public static class EnumExtensions
{
    public static Result<TEnum> TryParseEnum<TEnum>(string input, string? nameForError = null) where TEnum : struct, Enum
    {
        return Enum.TryParse<TEnum>(input, ignoreCase: true, out var value)
            ? Result<TEnum>.Success(value)
            : Result<TEnum>.Failure(IrtError.Validation($"Invalid {nameForError ?? typeof(TEnum).Name} value: '{input}'"));
    }
}