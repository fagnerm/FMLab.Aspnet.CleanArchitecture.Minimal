// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListUsers;

public record ListUsersInputDTO(
    UserStatus? Status,
    int? Page = 1,
    int? PageSize = 20);