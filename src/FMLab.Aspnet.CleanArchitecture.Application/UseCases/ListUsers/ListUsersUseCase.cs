// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.Enums;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public class ListUsersUseCase : IListUsersUseCase
{
    private readonly IUserGateway _gateway;

    public ListUsersUseCase(IUserGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<ListUsersOutputDTO> ExecuteAsync(ListUsersInputDTO input, CancellationToken ct)
    {
        UserStatus? status = Enum.TryParse<UserStatus>(input.Status, true, out var parsedStatus)
                                ? parsedStatus
                                : null;

        var filter = new ListUsersFilter(status, input.Page ?? 1, input.PageSize ?? 20);
        var result = await _gateway.ListAsync(filter, ct);

        return new ListUsersOutputDTO(result.Items, result.Page, result.PageSize, result.TotalPages, result.TotalCount);
    }
}
