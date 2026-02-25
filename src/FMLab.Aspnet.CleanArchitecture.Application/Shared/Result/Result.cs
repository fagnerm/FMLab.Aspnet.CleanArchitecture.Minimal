// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public string? Error { get; protected set; }
    public object Data { get; protected set; }
    public ResultType Type { get; protected set; }

    private Result(ResultType type = ResultType.Success)
    {
        Type = type;
    }

    private Result(string? error, ResultType type)
    {
        IsSuccess = false;
        Error = error;
        Type = type;
    }

    public static Result Success<TOutput>(TOutput? data = default)
    {
        return new Result(ResultType.Success)
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static Result NotFound(string? error)
    {
        return new Result(error, ResultType.NotFound);
    }

    public static Result Validation(string? error)
    {
        return new Result(error, ResultType.Validation);
    }

    public static Result Domain(string? error)
    {
        return new Result(error, ResultType.Domain);
    }

    public static Result Conflict(string? error)
    {
        return new Result(error, ResultType.Conflict);
    }

}
     

public enum ResultType
{
    Success,
    NoContent,
    NotFound,
    Validation,
    Domain,
    Conflict
}