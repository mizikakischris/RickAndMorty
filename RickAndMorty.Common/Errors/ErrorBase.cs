using FluentResults;

namespace RickAndMorty.Common.Errors;

public abstract class ErrorBase : IError
{
    public string Message { get; }
    public List<IError> Reasons { get; }
    public Dictionary<string, object> Metadata { get; }
    public abstract string Title { get; }
    public abstract int StatusCode { get; }

    protected ErrorBase(string message)
    {
        Message = message;
        Metadata = new Dictionary<string, object> { { "Code", Title } };
        Reasons = [];
    }
}