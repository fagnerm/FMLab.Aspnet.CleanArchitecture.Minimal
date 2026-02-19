// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases.Shared;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

public abstract class TransactionalUseCaseBase<TInput, TOutput> : IUseCase<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalUseCaseBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UseCaseResult<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken)
    {
        var result = await ExecuteHandlerAsync(input, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return result;
    }

    public abstract Task<UseCaseResult<TOutput>> ExecuteHandlerAsync(TInput input, CancellationToken cancellationToken);
}
