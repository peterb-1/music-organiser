namespace Client.Utils;

public abstract class Maybe<T>
{
    private Maybe() {}

    public sealed class Success(T value) : Maybe<T>
    {
        public T Value { get; } = value;
    }

    public sealed class Failure(string error) : Maybe<T>
    {
        public string Error { get; } = error;
    }
}

public static class Maybe
{
    public static Maybe<T> Success<T>(T value) => new Maybe<T>.Success(value);
    public static Maybe<T> Failure<T>(string error) => new Maybe<T>.Failure(error);

    public static async Task<Maybe<T>> Then<T>(this Task<Maybe<T>> task, Action<T> action)
    {
        var maybe = await task;

        if (maybe is Maybe<T>.Success success)
        {
            action(success.Value);
        }

        return maybe;
    }

    public static async Task<Maybe<T>> Catch<T>(this Task<Maybe<T>> task, Action<string> action)
    {
        var maybe = await task;

        if (maybe is Maybe<T>.Failure failure)
        {
            action(failure.Error);
        }

        return maybe;
    }

    public static async Task Finally<T>(this Task<Maybe<T>> task, Action action)
    {
        await task;

        action();
    }
}
