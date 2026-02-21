// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

public abstract class UseCaseResult<TOutput>
    where TOutput : class
{
    public bool IsSuccess { get; protected set; }
    public string? Error { get; protected set; }
    public IReadOnlyCollection<string> Messages { get; protected set; } = new List<string>();
    public TOutput? Data { get; protected set; } = default;

    public static UseCaseResult<TOutput> Success(TOutput? data = default, string? message = default)
    {
        return new SuccessOutput<TOutput>(data, message);
    }

    public static UseCaseResult<TOutput> Failure(TOutput? data = default, string? error = null)
    {
        return new FailureOutput<TOutput>(data, error);
    }

    public void AddMessage(string message)
    {
        if (string.IsNullOrEmpty(message)) return;

        Messages.Append(message);
    }
}

public class SuccessOutput<TOutput> : UseCaseResult<TOutput>
    where TOutput : class
{
    public SuccessOutput(TOutput? data, string? message)
    {
        IsSuccess = true;
        Data = data;
        Messages.Append(message);
    }
}

public class FailureOutput<TOutput> : UseCaseResult<TOutput>
    where TOutput : class
{
    public FailureOutput(TOutput? data, string? error)
    {
        IsSuccess = false;
        Error = error;
        Data = data;
    }
}
