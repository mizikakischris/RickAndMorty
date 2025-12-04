namespace RickAndMorty.Common.Exceptions;

public class RickAndMortyApiException<T> : Exception
{
    public long Id { get; }
    public string ExceptionType { get; }
    public DateTime CreatedAt { get; }
    public T Result { get; }

    internal RickAndMortyApiException(long id, string message, DateTime createdAt, T result, string exceptionType)
        : base(message)
    {
        Id = id;
        CreatedAt = createdAt;
        Result = result;
        ExceptionType = exceptionType;
    }
}