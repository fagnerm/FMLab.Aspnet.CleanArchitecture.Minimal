// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FMLab.Aspnet.CleanArchitecture.Api.Configurations;

public static class AppEndpoints
{
    public static WebApplication AddApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/categories", ListAllCategoriesEndpoint);

        app.MapPost("/categories", CreateCategoryEndpoint);

        return app;
    }

    private static async Task ListAllCategoriesEndpoint(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static async Task<IResult> CreateCategoryEndpoint([FromServices] ICreateCategoryUseCase useCase, CancellationToken ct, string categoryName)
    {
        var input = new CreateCategoryInput(categoryName);
        var output = await useCase.ExecuteAsync(input, ct);

        return output.IsSuccess
                ? Results.Created()
                : Results.Conflict(output.Error);
    }
}
