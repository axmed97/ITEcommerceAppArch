using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Utilities.Results.Concret;

public class DataResult<T> : Result, IDataResult<T>
{
    public T? Data { get; }

    public DataResult(T? data, HttpStatusCode statusCode, bool success, string? message) : base(statusCode, success, message)
    {
        Data = data;
    }

    public DataResult(T? data, HttpStatusCode statusCode, bool success) : base(statusCode, success)
    {
        Data = data;
    }
}
