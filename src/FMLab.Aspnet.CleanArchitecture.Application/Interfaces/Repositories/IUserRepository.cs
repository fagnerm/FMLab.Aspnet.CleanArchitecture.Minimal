// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Users;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> ExistsAsync(Name name);
    Task<User?> GetByIdAsync(int id);
    Task UpdateAsync(User user);
}
