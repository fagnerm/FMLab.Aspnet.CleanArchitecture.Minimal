// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.Shared;

public class PageResult<TItems>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; set; }
    public IReadOnlyCollection<TItems> Items { get; init; }

    public PageResult(IReadOnlyCollection<TItems> items, int page, int pageSize, int totalCount)
    {
        Items = items ??= [];
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
