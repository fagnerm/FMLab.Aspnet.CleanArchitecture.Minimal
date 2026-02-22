// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Users;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken token);
    void Delete(User user);
    Task<bool> ExistsAsync(Name name, CancellationToken token);
    Task<User?> GetByIdAsync(int id, CancellationToken token);
    User Update(User user);
}
