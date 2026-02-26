// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;

namespace FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;

public interface IUseCase<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    Task<Result> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}
