// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Domain.Users;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(User User)
    {
        await _dbContext.AddAsync(User);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(User User)
    {
        var exists = await _dbContext
                            .Users
                            .AnyAsync(_ => _.Name == User.Name);

        return exists;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var User = await _dbContext
                            .Users
                            .FirstOrDefaultAsync(_ => _.Id == id);

        return User;
    }

    public async Task UpdateAsync(User User)
    {
        _dbContext.Update(User);
        await _dbContext.SaveChangesAsync();
    }
}
