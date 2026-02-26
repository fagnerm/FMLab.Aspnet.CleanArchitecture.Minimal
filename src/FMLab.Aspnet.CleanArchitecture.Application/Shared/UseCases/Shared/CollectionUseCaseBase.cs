// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Shared.Filter;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;

namespace FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;

public abstract class CollectionUseCaseBase<TInput, TOutput, TItem> : ICollectionUseCase<TInput, TOutput, TItem>
    where TInput : PaginationFilter
    where TItem : class
    where TOutput : CollectionResult<TItem>
{
    public abstract Task<Result> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}
