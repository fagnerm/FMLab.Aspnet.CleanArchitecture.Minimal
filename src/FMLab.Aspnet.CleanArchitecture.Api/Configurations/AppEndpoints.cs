// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DeleteUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;
using Microsoft.AspNetCore.Mvc;


namespace FMLab.Aspnet.CleanArchitecture.Api.Configurations;

public static class AppEndpoints
{
    public static WebApplication UseApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/users", ListAllUsersEndpoint);
        app.MapGet("/users/{id}", ListUserEndpoint);

        app.MapPost("/users/{id}/disable", DisableUserEndpoint);

        app.MapPost("/users", PostUserEndpoint);

        app.MapPatch("/users/{id}", PatchUserEndpoint);

        app.MapPut("/users/{id}", PutUserEndpoint);

        app.MapDelete("/users/{id}", DeleteUserEndpoint);

        return app;
    }

    private static async Task<IResult> ListAllUsersEndpoint([FromServices] IListUsersUseCase useCase, [AsParameters] ListUsersInputDTO input, CancellationToken token)
    {
        var output = await useCase.ExecuteAsync(input, token);

        return Results.Ok(output);
    }

    private static async Task<IResult> ListUserEndpoint([FromServices] IGetUserUseCase useCase, int id, CancellationToken token)
    {
        var input = new GetUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, token);

        return Results.Ok(output);
    }


    private static async Task<IResult> PostUserEndpoint([FromServices] ICreateUserUseCase useCase, [FromQuery] string name, string? email, CancellationToken ct)
    {
        var input = new CreateUserInputDTO(name, email);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created($"users/{output?.Data?.Id}", output?.Data)
                : Results.UnprocessableEntity(output.Error);
    }

    private static async Task<IResult> DisableUserEndpoint([FromServices] IDisableUserUseCase useCase, [FromRoute] int id, CancellationToken ct)
    {
        var input = new DisableUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Accepted()
                : Results.NotFound(output.Error);
    }

    private static async Task<IResult> PutUserEndpoint([FromServices] IUpdateUserUseCase useCase, [FromQuery] int id, [FromBody] UpdateUserInputRequest body, CancellationToken ct)
    {
        var input = new UpdateUserInputDTO(id, body.Name, body.Email);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Accepted($"users/{output?.Data?.Id}", output?.Data)
                : Results.UnprocessableEntity(output.Error);
    }

    private static async Task<IResult> PatchUserEndpoint([FromServices] IPatchUserUseCase useCase, [FromQuery] int id, [FromBody] UpdateUserInputRequest body, CancellationToken ct)
    {
        var input = new UpdateUserInputDTO(id, body.Name, body.Email);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created($"users/{output?.Data?.Id}", output?.Data)
                : Results.UnprocessableEntity(output.Error);
    }

    private static async Task<IResult> DeleteUserEndpoint([FromServices] IDeleteUserUseCase useCase, [FromQuery] int id, CancellationToken ct)
    {
        var input = new DeleteUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created($"users/{output?.Data?.Id}", output?.Data)
                : Results.NotFound(output.Error);
    }
}
