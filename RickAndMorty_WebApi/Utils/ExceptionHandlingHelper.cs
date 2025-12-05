using FluentResults;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Common.Exceptions;

namespace RickAndMorty.Common.Utils;

public static class ExceptionHandlingHelper
{
    /// <summary>
    /// Executes an action that does not return a value, handling ApiExceptions<TDetails> and generic exceptions.
    /// </summary>
    public static async Task<Result> ExecuteWithApiExceptionHandling(Func<Task> action)
    {
        try
        {
            await action();
            return Result.Ok();
        }
        catch (RickAndMortyApiException<ValidationProblemDetails> ex)
        {
            return Result.Fail(ex.Result.Errors.SelectMany(e => e.Value));
        }
        catch (RickAndMortyApiException<ProblemDetails> ex)
        {
            return Result.Fail($@"{ex.Result.Title}: {ex.Result.Detail}");
        }
    }

    /// <summary>
    /// Executes an operation returning a generic Result<T> with API exception handling.
    /// </summary>
    public static async Task<Result<T>> ExecuteWithApiExceptionHandling<T>(Func<Task<Result<T>>> action)
        where T : class
    {
        return await ExecuteWithApiExceptionHandling(action, new Dictionary<string, object>());
    }

    /// <summary>
    /// Executes an operation returning a generic Result<T> with API exception handling, including metadata.
    /// </summary>
    public static async Task<Result<T>> ExecuteWithApiExceptionHandling<T>(Func<Task<Result<T>>> action, Dictionary<string, object> metadata)
        where T : notnull
    {
        try
        {
            return await action();
        }
        catch (RickAndMortyApiException<ValidationProblemDetails> ex)
        {
            return Result.Fail<T>(ex.Result.Errors.SelectMany(e => e.Value));
        }
        catch (RickAndMortyApiException<ProblemDetails> ex)
        {
            return Result.Fail(new Error($@"{ex.Result.Title}: {ex.Result.Detail}").WithMetadata(metadata).CausedBy(ex));
        }
    }
}
