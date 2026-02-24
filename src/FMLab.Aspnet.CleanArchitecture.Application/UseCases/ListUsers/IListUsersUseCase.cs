// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListUsers;

public interface IListUsersUseCase : ICollectionUseCase<ListUsersInputDTO, ListUsersOutputDTO, UserSummaryDTO>
{
}
