using Core.Utilities.Results.Abstract;
using System.Net;

namespace Core.Utilities.Results.Concret;

public class Result : IResult
{
    public HttpStatusCode StatusCode { get; }

    public bool Success { get; }

    public string? Message { get; }

    public Result(HttpStatusCode statusCode, bool success, string? message) : this(statusCode, success)
    {
        Message = message;
    }

    public Result(HttpStatusCode statusCode, bool success)
    {
        StatusCode = statusCode;
        Success = success;
    }

}
