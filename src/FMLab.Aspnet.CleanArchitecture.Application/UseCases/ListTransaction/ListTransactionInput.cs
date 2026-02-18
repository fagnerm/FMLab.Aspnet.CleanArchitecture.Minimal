// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public record ListTransactionInput(
    TransactionType? Type,
    DateTime? StartDate,
    DateTime? EndDate,
    int? Page = 1,
    int? PageSize = 20);