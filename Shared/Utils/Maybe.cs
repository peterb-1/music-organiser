namespace Shared.Utils;

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

    public static async Task<Maybe<T>> Then<T>(this Task<Maybe<T>> task, Func<T, Task> action)
    {
        var maybe = await task;

        if (maybe is Maybe<T>.Success success)
        {
            await action(success.Value);
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

    public static async Task<Maybe<T>> Catch<T>(this Task<Maybe<T>> task, Func<string, Task> action)
    {
        var maybe = await task;

        if (maybe is Maybe<T>.Failure failure)
        {
            await action(failure.Error);
        }

        return maybe;
    }

    public static async Task Finally<T>(this Task<Maybe<T>> task, Action action)
    {
        await task;

        action();
    }

    public static async Task Finally<T>(this Task<Maybe<T>> task, Func<Task> action)
    {
        await task;

        await action();
    }

    public static TResult Match<T, TResult>(this Maybe<T> maybe, Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
    {
        return maybe switch
        {
            Maybe<T>.Success success => onSuccess(success.Value),
            Maybe<T>.Failure failure => onFailure(failure.Error),
            _ => throw new InvalidOperationException("Unreachable")
        };
    }

    public static async Task<TResult> Match<T, TResult>(this Maybe<T> maybe, Func<T, Task<TResult>> onSuccess, Func<string, TResult> onFailure)
    {
        return maybe switch
        {
            Maybe<T>.Success success => await onSuccess(success.Value),
            Maybe<T>.Failure failure => onFailure(failure.Error),
            _ => throw new InvalidOperationException("Unreachable")
        };
    }

    public static async Task<TResult> Match<T, TResult>(this Maybe<T> maybe, Func<T, TResult> onSuccess, Func<string, Task<TResult>> onFailure)
    {
        return maybe switch
        {
            Maybe<T>.Success success => onSuccess(success.Value),
            Maybe<T>.Failure failure => await onFailure(failure.Error),
            _ => throw new InvalidOperationException("Unreachable")
        };
    }

    public static async Task<TResult> Match<T, TResult>(this Maybe<T> maybe, Func<T, Task<TResult>> onSuccess, Func<string, Task<TResult>> onFailure)
    {
        return maybe switch
        {
            Maybe<T>.Success success => await onSuccess(success.Value),
            Maybe<T>.Failure failure => await onFailure(failure.Error),
            _ => throw new InvalidOperationException("Unreachable")
        };
    }

    public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
    {
        var maybe = await task;

        return maybe.Match(onSuccess, onFailure);
    }

    public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, Task<TResult>> onSuccess, Func<string, TResult> onFailure)
    {
        var maybe = await task;

        return await maybe.Match(onSuccess, onFailure);
    }

    public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, TResult> onSuccess, Func<string, Task<TResult>> onFailure)
    {
        var maybe = await task;

        return await maybe.Match(onSuccess, onFailure);
    }

    public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, Task<TResult>> onSuccess, Func<string, Task<TResult>> onFailure)
    {
        var maybe = await task;

        return await maybe.Match(onSuccess, onFailure);
    }
}
