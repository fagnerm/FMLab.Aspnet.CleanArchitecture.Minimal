// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.DTOs;

public record TransactionSummaryDTO(
    int Id,
    string Currency,
    decimal Amount,
    string Type,
    DateTime CreatedAt
    );
