using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Utilities.Results.Concret.ErrorResults;

public class ErrorResult : Result
{
    public ErrorResult(HttpStatusCode statusCode) : base(statusCode, false)
    {
    }

    public ErrorResult(HttpStatusCode statusCode, string? message) : base(statusCode, false, message)
    {
    }
}
