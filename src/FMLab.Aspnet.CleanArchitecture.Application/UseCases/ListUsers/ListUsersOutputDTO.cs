// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListUsers;

public record ListUsersOutputDTO(IReadOnlyCollection<UserSummaryDTO> Items, int Page, int PageSize, int TotalItems)
    : CollectionResult<UserSummaryDTO>(Items, Page, PageSize, TotalItems), IResultData;