// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Domain.Exceptions;

namespace FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases.Shared;

public abstract class TransactionalUseCaseBase<TInput, TOutput> : IUseCase<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalUseCaseBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken)
    {
        try
        {
            var result = await ExecuteHandlerAsync(input, cancellationToken);

            if (result.IsSuccess)
                await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
        catch (DomainException ex)
        {
            return Result<TOutput>.Domain(ex.Message);
        }
    }

    public abstract Task<Result<TOutput>> ExecuteHandlerAsync(TInput input, CancellationToken cancellationToken);
}
