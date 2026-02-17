// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(Category category)
    {
        await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CategoryExistsAsync(Category category)
    {
        var exists = await _dbContext
                            .Categories
                            .AnyAsync(_ => _.Name == category.Name);

        return exists;
    }
}
