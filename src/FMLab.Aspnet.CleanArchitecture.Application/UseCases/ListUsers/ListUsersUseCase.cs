// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListUsers;

public class ListUsersUseCase : CollectionUseCaseBase<ListUsersInputDTO, ListUsersOutputDTO, UserSummaryDTO>, IListUsersUseCase
{
    private readonly IUserGateway _gateway;

    public ListUsersUseCase(IUserGateway gateway)
    {
        _gateway = gateway;
    }

    public async override Task<Result<ListUsersOutputDTO>> ExecuteAsync(ListUsersInputDTO input, CancellationToken cancellationToken)
    {
        var filter = new ListUsersFilter(input.Status, input.Page, input.PageSize);
        var result = await _gateway.ListAsync(filter, cancellationToken);

        var output = new ListUsersOutputDTO(result.Items, result.Page, result.PageSize, result.TotalItems);
        return Result<ListUsersOutputDTO>.Success(output);
    }
}
