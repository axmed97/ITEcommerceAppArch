using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace Core.Utilities.Results.Concret.SuccessResults;

public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(HttpStatusCode statusCode, T? data) : base(data, statusCode, true)
    {
    }

    public SuccessDataResult(HttpStatusCode statusCode, T? data, string? message) : base(data, statusCode, true, message)
    {
    }

    public SuccessDataResult(HttpStatusCode statusCode) : base(default, statusCode, true)
    {
    }

    public SuccessDataResult(HttpStatusCode statusCode, string? message) : base(default, statusCode, true, message)
    {
    }
}
