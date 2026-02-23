// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;

public class ListResult<TItems>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; }
    public IReadOnlyCollection<TItems> Items { get; init; }

    public ListResult(IReadOnlyCollection<TItems> items, int page, int pageSize, int totalCount)
    {
        Items = items ??= [];
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
