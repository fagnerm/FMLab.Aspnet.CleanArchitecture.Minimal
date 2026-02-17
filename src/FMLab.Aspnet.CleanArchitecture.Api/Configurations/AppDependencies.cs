// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Repositories;

namespace FMLab.Aspnet.CleanArchitecture.Api.Configurations;

public static class AppDependencies
{
    public static WebApplicationBuilder AddApplicationDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ITransactionRepository, TransactionRepository>();

        builder.Services
            .AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>()
            .AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();

        return builder;
    }
}
