// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Api.Endpoints.Helpers;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.CreateUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DeleteUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DisableUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.GetUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListUsers;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace FMLab.Aspnet.CleanArchitecture.Api.Endpoints.Users;

internal static class UserEndpoints
{
    internal static void MapUser(WebApplication app)
    {
        app.MapGet("/users", ListAllUsersEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi();

        app.MapGet("/users/{id}", ListUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .WithOpenApi();

        app.MapPost("/users/{id}/deactivate", DisableUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .WithOpenApi();

        app.MapPost("/users", PostUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status409Conflict)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi();

        app.MapPatch("/users/{id}", PatchUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi();

        app.MapPut("/users/{id}", PutUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi();

        app.MapDelete("/users/{id}", DeleteUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .WithOpenApi();
    }

    private static async Task<IResult> ListAllUsersEndpoint([FromServices] IListUsersUseCase useCase, [AsParameters] ListUsersInputDTO input, CancellationToken token)
    {
        var output = await useCase.ExecuteAsync(input, token);

        return output.ToProblemResult();
    }

    private static async Task<IResult> ListUserEndpoint([FromServices] IGetUserUseCase useCase, [FromRoute]int id, CancellationToken token)
    {
        var input = new GetUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, token);

        return output.ToProblemResult();
    }

    private static async Task<IResult> PostUserEndpoint([FromServices] ICreateUserUseCase useCase, [FromBody] CreateUserInputRequest body, CancellationToken ct)
    {
        var input = new CreateUserInputDTO(body.Name, body.Email);
        var output = await useCase.ExecuteAsync(input, ct);

        if (!output.IsSuccess) return output.ToProblemResult();

        return Results.Created($"/users/{output.Data?.Id}", output.Data);
    }

    private static async Task<IResult> DisableUserEndpoint([FromServices] IDisableUserUseCase useCase, [FromRoute] int id, CancellationToken ct)
    {
        var input = new DisableUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.ToProblemResult(ResultType.NoContent);
    }

    private static async Task<IResult> PutUserEndpoint([FromServices] IUpdateUserUseCase useCase, [FromRoute] int id, [FromBody] UpdateUserInputRequest body, CancellationToken ct)
    {
        var input = new UpdateUserInputDTO(id, body.Name, body.Email);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.ToProblemResult();
    }

    private static async Task<IResult> PatchUserEndpoint([FromServices] IPatchUserUseCase useCase, [FromRoute] int id, [FromBody] UpdateUserInputRequest body, CancellationToken ct)
    {
        var input = new UpdateUserInputDTO(id, body.Name, body.Email);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.ToProblemResult();
    }

    private static async Task<IResult> DeleteUserEndpoint([FromServices] IDeleteUserUseCase useCase, [FromRoute] int id, CancellationToken ct)
    {
        var input = new DeleteUserInputDTO(id);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.ToProblemResult(ResultType.NoContent);
    }
}
