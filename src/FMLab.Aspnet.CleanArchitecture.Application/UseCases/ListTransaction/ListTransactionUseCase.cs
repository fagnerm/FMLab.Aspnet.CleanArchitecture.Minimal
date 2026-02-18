// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;

public class ListTransactionUseCase : IListTransactionUseCase
{
    private readonly ITransactionGateway _gateway;

    public ListTransactionUseCase(ITransactionGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<ListTransactionOutput> ExecuteAsync(ListTransactionInput input, CancellationToken ct)
    {
        var filter = new ListTransactionFilter(input.Type, input.StartDate, input.EndDate, input.Page.Value, input.PageSize.Value);
        var result = await _gateway.ListAsync(filter, ct);

        return new ListTransactionOutput(result.Items, result.TotalCount, result.Page, result.PageSize);
    }
}
