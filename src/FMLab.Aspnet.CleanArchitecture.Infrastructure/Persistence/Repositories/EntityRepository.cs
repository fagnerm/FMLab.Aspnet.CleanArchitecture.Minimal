// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Repositories;

public class EntityRepository : IEntityRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EntityRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(Entity entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> EntityExistsAsync(Entity entity)
    {
        var exists = await _dbContext
                            .Entities
                            .AnyAsync(_ => _.Name == entity.Name);

        return exists;
    }
}
