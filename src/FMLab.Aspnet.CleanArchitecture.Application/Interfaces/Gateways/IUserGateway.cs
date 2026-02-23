// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;

public interface IUserGateway
{
    Task<ListResult<UserSummaryDTO>> ListAsync(ListUsersFilter filter, CancellationToken token);
    Task<UserSummaryDTO?> ListUserByIdAsync(int id, CancellationToken token);
    Task<bool> ExistsByKeyAsync(Name? name, Email? email, CancellationToken token);
}
