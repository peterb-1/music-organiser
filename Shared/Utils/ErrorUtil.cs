namespace Shared.Utils;

public static class ErrorUtil
{
    public static string FlattenErrors<TError>(this IEnumerable<TError> errors, Func<TError, string> extractor)
    {
        return string.Join(" ", errors.Select(extractor));
    }
}
