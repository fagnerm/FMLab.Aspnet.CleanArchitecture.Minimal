// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Gateways;

public class UserGateway : IUserGateway
{
    private readonly ApplicationDbContext _context;

    public UserGateway(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<UserSummaryDTO>> ListAsync(ListUsersFilter filter, CancellationToken ct)
    {
        var query = _context.Users
                            .AsNoTracking()
                            .AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(t => t.Status == filter.Status.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query.OrderByDescending(t => t.Name)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new UserSummaryDTO(
                t.Id,
                t.Name.Value,
                t.Status.ToString()
                ))
            .ToListAsync();

        return new PageResult<UserSummaryDTO>(
            items, filter.Page, filter.PageSize, totalCount);
    }
}
