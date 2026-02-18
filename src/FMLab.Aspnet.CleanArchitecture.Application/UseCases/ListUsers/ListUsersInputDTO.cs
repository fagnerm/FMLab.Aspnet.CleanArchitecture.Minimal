// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public record ListUsersInputDTO(
    string? Status,
    int? Page = 1,
    int? PageSize = 20);