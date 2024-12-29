using System.Runtime.CompilerServices;

namespace SimpleApi;

public static class GuardExtensions
{
    public static T ThrowIfNull<T>(this T parameter,
        string? message = null,
        [CallerArgumentExpression("parameter")] string parameterName = null!)
    {
        if (parameter == null)
        {
            throw new ArgumentNullException(parameterName, message ?? $"Parameter '{parameterName}' is null!");
        }
        return parameter;
    }
}
