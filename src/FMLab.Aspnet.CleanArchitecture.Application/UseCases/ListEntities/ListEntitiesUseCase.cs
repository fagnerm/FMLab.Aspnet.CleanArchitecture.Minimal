// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;

public class ListEntitiesUseCase : IListEntitiesUseCase
{
    private readonly ITransactionGateway _gateway;

    public ListEntitiesUseCase(ITransactionGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<ListEntitiesOutputDTO> ExecuteAsync(ListEntitiesInputDTO input, CancellationToken ct)
    {
        var filter = new ListEntitiesFilter(input.Status, input.Page.Value, input.PageSize.Value);
        var result = await _gateway.ListAsync(filter, ct);

        return new ListEntitiesOutputDTO(result.Items, result.Page, result.PageSize, result.TotalPages, result.TotalCount);
    }
}
