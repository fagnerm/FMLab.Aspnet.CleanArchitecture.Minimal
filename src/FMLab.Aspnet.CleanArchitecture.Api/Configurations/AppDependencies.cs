// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListTransaction;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Gateways;
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
            .AddScoped<ITransactionGateway, TransactionGateway>();

        builder.Services
            .AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>()
            .AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>()
            .AddScoped<IListTransactionUseCase, ListTransactionUseCase>();

        return builder;
    }
}
