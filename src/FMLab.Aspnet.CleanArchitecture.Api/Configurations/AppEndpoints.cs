// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DisableEntity;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace FMLab.Aspnet.CleanArchitecture.Api.Configurations;

public static class AppEndpoints
{
    public static WebApplication AddApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/entities", ListAllEntitiesEndpoint);

        app.MapPost("/entities", CreateEntityEndpoint);

        app.MapPut("/entities/{id}/disable", DisableEntityEndpoint);

        return app;
    }

    private static async Task<IResult> ListAllEntitiesEndpoint([FromServices] IListEntitiesUseCase useCase, [AsParameters] ListEntitiesInputDTO input, CancellationToken token)
    {
        var output = await useCase.ExecuteAsync(input, token);

        return Results.Ok(output);
    }

    private static async Task<IResult> CreateEntityEndpoint([FromServices] ICreateEntityUseCase useCase, [FromQuery] string name, CancellationToken ct)
    {
        var input = new CreateEntityInputDTO(name);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created()
                : Results.Conflict(output.Error);
    }

    private static async Task<IResult> DisableEntityEndpoint([FromServices] IDisableEntityUseCase useCase, [FromRoute] int id, CancellationToken ct)
    {
        var input = new DisableEntityInputDTO(id);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Accepted()
                : Results.NotFound(output.Error);
    }
}
