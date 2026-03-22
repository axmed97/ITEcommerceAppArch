using System.Net;

namespace Core.Utilities.Results.Concret.ErrorResults;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(HttpStatusCode statusCode, T? data) : base(data, statusCode, false)
    {
    }

    public ErrorDataResult(HttpStatusCode statusCode, T? data, string? message) : base(data, statusCode, false, message)
    {
    }

    public ErrorDataResult(HttpStatusCode statusCode) : base(default, statusCode, false)
    {
    }

    public ErrorDataResult(HttpStatusCode statusCode, string? message) : base(default, statusCode, false, message)
    {
    }
}
