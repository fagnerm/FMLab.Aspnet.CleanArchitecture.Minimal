// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces;

public interface IUseCase<TInput>
{
    Task<UseCaseResult> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}
