// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;

public interface IUserGateway
{
    Task<PageResult<UserSummaryDTO>> ListAsync(ListUsersFilter filter, CancellationToken ct);
}
