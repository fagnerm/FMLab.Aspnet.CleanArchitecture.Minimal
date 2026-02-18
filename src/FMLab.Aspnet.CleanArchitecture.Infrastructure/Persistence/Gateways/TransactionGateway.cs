// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Shared;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Gateways;

public class TransactionGateway : ITransactionGateway
{
    private readonly ApplicationDbContext _context;

    public TransactionGateway(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<TransactionSummaryDTO>> ListAsync(ListTransactionFilter filter, CancellationToken ct)
    {
        var query = _context.Transactions
                            .AsNoTracking()
                            .AsQueryable();

        if (filter.Type.HasValue)
        {
            query.Where(t => t.Type == filter.Type.Value);
        }

        if (filter.StartAt.HasValue)
        {
            query.Where(t => t.CreatedAt >= filter.StartAt.Value);
        }

        if (filter.EndAt.HasValue)
        {
            query.Where(t => t.CreatedAt <= filter.EndAt.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query.OrderByDescending(t => t.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new TransactionSummaryDTO(
                t.Id,
                t.Amount.Currency,
                t.Amount.Value,
                t.Type.ToString(),
                t.CreatedAt
                ))
            .ToListAsync();

        return new PageResult<TransactionSummaryDTO>(
            items, filter.Page, filter.PageSize, totalCount);
    }
}
