// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Shared;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;

public interface ITransactionGateway
{
    Task<PageResult<TransactionSummaryDTO>> ListAsync(ListTransactionFilter filter, CancellationToken ct);
}
