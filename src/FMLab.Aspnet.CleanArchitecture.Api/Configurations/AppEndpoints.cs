// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;
using Microsoft.AspNetCore.Mvc;

namespace FMLab.Aspnet.CleanArchitecture.Api.Configurations;

public static class AppEndpoints
{
    public static WebApplication AddApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/entities", ListAllEntitiesEndpoint);

        app.MapPost("/entities", CreateEntityEndpoint);

        app.MapPut("/entities/{Id}/disable", DisableEntityEndpoint);

        return app;
    }

    private static async Task<IResult> ListAllEntitiesEndpoint([FromServices]IListEntitiesUseCase useCase, [AsParameters]ListEntitiesFilter filter, CancellationToken token)
    {

        var input = new ListEntitiesInputDTO(
            Status: filter.Status,
            Page: filter.Page,
            PageSize: filter.PageSize);

        var output = await useCase.ExecuteAsync(input, token);

        return Results.Ok(output);
    }

    private static async Task<IResult> CreateEntityEndpoint([FromServices] ICreateEntityUseCase useCase, [FromQuery]string name, CancellationToken ct)
    {
        var input = new CreateEntityInputDTO(name);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created()
                : Results.Conflict(output.Error);
    }

    private static async Task DisableEntityEndpoint(HttpContext context)
    {
        throw new NotImplementedException();
    }
}
