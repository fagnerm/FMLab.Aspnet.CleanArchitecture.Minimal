// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using Microsoft.AspNetCore.Mvc;


namespace FMLab.Aspnet.CleanArchitecture.Api.Configurations;

public static class AppEndpoints
{
    public static WebApplication UseApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/users", ListAllUsersEndpoint);

        app.MapPost("/users", CreateUserEndpoint);

        app.MapPut("/users/{id}/disable", DisableUserEndpoint);

        return app;
    }

    private static async Task<IResult> ListAllUsersEndpoint([FromServices] IListUsersUseCase useCase, [AsParameters] ListUsersInputDTO input, CancellationToken token)
    {
        var output = await useCase.ExecuteAsync(input, token);

        return Results.Ok(output);
    }

    private static async Task<IResult> CreateUserEndpoint([FromServices] ICreateUserUseCase useCase, [FromQuery] string name, string email, CancellationToken ct)
    {
        var input = new CreateUserInputDTO(name, email);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created($"users/{output?.Data?.Id}", output?.Data)
                : Results.Conflict(output.Error);
    }

    private static async Task<IResult> DisableUserEndpoint([FromServices] IDisableUserUseCase useCase, [FromRoute] int id, CancellationToken ct)
    {
        var input = new DisableUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Accepted()
                : Results.NotFound(output.Error);
    }
}
