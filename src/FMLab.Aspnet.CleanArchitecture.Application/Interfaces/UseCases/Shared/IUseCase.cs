// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases.Shared;

public interface IUseCase<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    Task<UseCaseResult<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}
