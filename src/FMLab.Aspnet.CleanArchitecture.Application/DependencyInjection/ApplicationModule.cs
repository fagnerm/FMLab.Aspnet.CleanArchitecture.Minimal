// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FMLab.Aspnet.CleanArchitecture.Application.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IDisableUserUseCase, DisableUserUseCase>();

        services.AddScoped<IListUsersUseCase, ListUsersUseCase>();

        return services;
    }
}
