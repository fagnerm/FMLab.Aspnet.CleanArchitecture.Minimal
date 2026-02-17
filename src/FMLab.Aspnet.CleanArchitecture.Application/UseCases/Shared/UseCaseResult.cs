// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

public abstract class UseCaseResult
{
    public bool IsSuccess { get; protected set; }
    public string? Error { get; protected set; }

    public static UseCaseResult Success()
    {
        return new SuccessOutput();
    }

    public static UseCaseResult Failure(string message)
    {
        return new FailureOutput(message);
    }
}

public class SuccessOutput : UseCaseResult
{
    public SuccessOutput()
    {
        IsSuccess = true;
    }
}

public class FailureOutput : UseCaseResult
{
    public FailureOutput(string message)
    {
        IsSuccess = false;
        Error = message;
    }
}
