// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

public abstract class UseCaseResult<TOutput>
    where TOutput : class
{
    public bool IsSuccess { get; protected set; }
    public string? Error { get; protected set; }

    public static UseCaseResult<TOutput> Success()
    {
        return new SuccessOutput<TOutput>();
    }

    public static UseCaseResult<TOutput> Failure(string message)
    {
        return new FailureOutput<TOutput>(message);
    }
}

public class SuccessOutput<TOutput> : UseCaseResult<TOutput>
    where TOutput : class
{
    public SuccessOutput()
    {
        IsSuccess = true;
    }
}

public class FailureOutput<TOutput> : UseCaseResult<TOutput>
    where TOutput : class
{
    public FailureOutput(string message)
    {
        IsSuccess = false;
        Error = message;
    }
}
