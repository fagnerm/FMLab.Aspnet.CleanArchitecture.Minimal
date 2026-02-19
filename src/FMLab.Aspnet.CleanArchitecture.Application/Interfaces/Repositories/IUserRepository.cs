// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Users;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User User);
    Task<bool> ExistsAsync(User User);
    Task<User?> GetByIdAsync(int id);
    Task UpdateAsync(User User);
}
