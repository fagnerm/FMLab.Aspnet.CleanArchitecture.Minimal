// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

public class Result<TOutput>
    where TOutput : class
{
    public bool IsSuccess { get; protected set; }
    public string? Error { get; protected set; }
    public TOutput? Data { get; protected set; } = default;
    public ResultType Type { get; protected set; }

    private Result(TOutput? data)
    {
        IsSuccess = true;
        Data = data;
    }

    private Result(string? error, ResultType type)
    {
        IsSuccess = false;
        Error = error;
        Type = type;
    }

    public static Result<TOutput> Success(TOutput? data = default)
    {
        return new Result<TOutput>(data);
    }

    public static Result<TOutput> NotFound(string? error)
    {
        return new Result<TOutput>(error, ResultType.NotFound);
    }

    public static Result<TOutput> Validation(string? error)
    {
        return new Result<TOutput>(error, ResultType.Validation);
    }

    public static Result<TOutput> Domain(string? error)
    {
        return new Result<TOutput>(error, ResultType.Validation);
    }

    public static Result<TOutput> Conflict(string? error)
    {
        return new Result<TOutput>(error, ResultType.Conflict);
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