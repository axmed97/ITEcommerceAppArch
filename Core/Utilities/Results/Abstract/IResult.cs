using System.Net;

namespace Core.Utilities.Results.Abstract;

public interface IResult
{
    HttpStatusCode StatusCode { get; }
    bool Success { get; }
    string? Message { get; }
}
