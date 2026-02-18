// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Entities;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;

public interface IEntityRepository
{
    Task AddAsync(Entity entity);
    Task<bool> ExistsAsync(Entity entity);
    Task<Entity?> GetByIdAsync(int id);
    Task UpdateAsync(Entity entity);
}
